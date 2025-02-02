using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPlayerAction
{
    public delegate void PlayerActionDelegate();
    public event PlayerActionDelegate PlayerActionEvent;

    public delegate void CancelActionDelegate();
    public event CancelActionDelegate CancelActionEvent;
}

public class PlayerAction : IPlayerAction
{
    private CurrentAction action;

    private bool onUI => EventSystem.current.IsPointerOverGameObject();

    public void SetSpell(SpellStatus spell, Relantioship team)
    {
        ClearSpell();
        action = new CurrentAction(spell, team);
    }

    public event IPlayerAction.PlayerActionDelegate PlayerActionEvent;
    public event IPlayerAction.CancelActionDelegate CancelActionEvent;

    public void ApplySpell()
    {
        if (onUI) return;
        if (action == null) return;
        if (!CursorPosition.RayHit().transform) return;

        if (!EqualTeam(CursorPosition.RayObject()))
        {
            Debug.LogWarning("Неверная цель");
            return;
        }

        ActionSelectot.SelectAction(CursorPosition.RayObject(), action._spell);
        if (action != null) action = null;

        PlayerActionEvent?.Invoke();
    }

    public void ClearSpell()
    {
        if (action == null) return;

        CancelActionEvent?.Invoke();
        action = null;
    }

    private bool EqualTeam(GameObject target)
    {
        if (!target.GetComponent<Team>()) return false;

        Relantioship targetTeam = target.GetComponent<ITeam>().GetRelantioship();
        bool equalTargets = targetTeam == action._team;

        if (action._spell.target == TargetType.Enemy && !equalTargets) return true;
        if (action._spell.target == TargetType.Ally && equalTargets) return true;
        return false;
    }
}

public class CurrentAction
{
    public readonly SpellStatus _spell;
    public readonly Relantioship _team;

    public CurrentAction(SpellStatus spell, Relantioship team)
    {
        _spell = spell;
        _team = team;
    }
}

public static class ActionSelectot
{
    public static void SelectAction(GameObject target, SpellStatus spell)
    {
        foreach (SpellEffects effect in spell.effects)
        {
            if (effect.type == SpellType.Damage)
                new DamageAction(target, spell);

            else if (effect.type == SpellType.ClearStatus)
                new CleansingAction(target, spell);

            else new EffectAction(target, spell);
        }
    }
}


