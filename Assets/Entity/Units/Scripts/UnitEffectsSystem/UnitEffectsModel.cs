using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IEffect
{
    public void ApplyEffect(SpellStatus spell);
    public void RemoveEffect(UnitEffect effect);
    public void EffectTurnLeft();
}

public interface IGetGroupEffects
{
    public float SummEffectsOfType(SpellType type);
    public void RemoveEffectsOfType(SpellType type);
}

public class UnitEffectsModel : IEffect, IGetGroupEffects
{
    [SerializeField] private List<UnitEffect> _effects = new List<UnitEffect>();
    GameObject _gameObject;

    public UnitEffectsModel(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

    public void ApplyEffect(SpellStatus spell)
    {
        UnitEffect effect = _effects.FirstOrDefault(e => e._nameID == spell.nameID);

        if (effect == null) AddEffect(spell);
        else ResetEffect(effect);
    }

    private void AddEffect(SpellStatus spell)
    {
        UnitEffect effect = new UnitEffect(spell);
        _effects.Add(effect);
        _gameObject.GetComponent<HealthBarCreator>().addEffectBar.AddEffect(effect);
    }

    public void RemoveEffect(UnitEffect effect)
    {
        _effects.Remove(effect);
    }

    private void ResetEffect(UnitEffect effect) => effect.ResetTurns();

    public void EffectTurnLeft()
    {
        foreach (UnitEffect effect in _effects) effect.TurnRemove();

        _effects.RemoveAll(n => n.TurnCheck());
    }

    public float SummEffectsOfType(SpellType type)
    {
        return _effects.SelectMany(effect => effect._spell.effects).Where(t => t.type == type).Sum(types => types.count);
    }

    public void RemoveEffectsOfType(SpellType type)
    {
        List<UnitEffect> list = _effects.Where(effect => effect._spell.effects.Any(t => t.type == type)).ToList();

        foreach (UnitEffect effect in list)
        {
            effect.Clear();
            _effects.Remove(effect);
        }
    }
}

[Serializable]
public class UnitEffect
{
    public readonly string _nameID;
    [SerializeField] public int _turnLeft;
    public readonly SpellStatus _spell;

    public UnitEffect(SpellStatus spell)
    {
        _spell = spell;
        _nameID = spell.nameID;
        _turnLeft = spell.duration;
    }

    public delegate void EffectClearDelegate();
    public event EffectClearDelegate EffectClearEvent;

    public delegate void ChangeTurnsDelegate();
    public event ChangeTurnsDelegate ChangeTurnsEvent;

    public void TurnRemove() => _turnLeft--;

    public bool TurnCheck() => _turnLeft <= 0;

    public void ResetTurns()
    {
        _turnLeft = _spell.duration;
        ChangeTurnsEvent?.Invoke();
    }

    public void Clear() 
    { 
        _turnLeft = 0;
        EffectClearEvent?.Invoke();
    }
}


