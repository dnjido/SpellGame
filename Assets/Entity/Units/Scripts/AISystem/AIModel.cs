using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IAIController
{
    public bool IsAI();
    public void AIAction();
}

public class AIModel : IAIController
{
    private bool _isAI;
    private readonly GameObject _gameObject;
    private readonly MonoBehaviour _monoBehaviour;

    public AIModel(bool isAI, GameObject gameObject, MonoBehaviour monoBehaviour) 
    {
        _isAI = isAI;
        _gameObject = gameObject;
        _monoBehaviour = monoBehaviour;
    }

    public bool IsAI() => _isAI;

    public void AIAction() => _monoBehaviour.StartCoroutine(AIDelay());

    IEnumerator AIDelay()
    {
        yield return new WaitForSeconds(.5f);
        SpellUse();
    }

    public void SpellUse()
    {
        List<SpellsCooldown> spells = _gameObject.GetComponent<IUnitSpellsAdapter>().ISpellsController.GetSpellList();
        spells = spells.Where(s => s._cooldown <= 0).ToList();

        int randomSpell = Random.Range(0, spells.Count);
        SpellsCooldown spell = spells[randomSpell];

        SelectTarget(spell._spell);
        spell.Use();
    }

    private void SelectTarget(SpellStatus spell)
    {
        if (spell.target == TargetType.Self)
        {
            ActionSelectot.SelectAction(_gameObject, spell);
            return;
        }

        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        if (spell.target == TargetType.Enemy)
        {
            units = units.Where(unit => !TeamEqual(unit)).ToArray();
        }
        else if (spell.target == TargetType.Ally)
        {
            units = units.Where(unit => TeamEqual(unit)).ToArray();
        }

        int randomEnemy = Random.Range(0, units.Length);
        ActionSelectot.SelectAction(units[randomEnemy], spell);

    }

    private bool TeamEqual(GameObject unit)
    {
        return unit.GetComponent<ITeam>().GetRelantioship() == _gameObject.GetComponent<ITeam>().GetRelantioship();
    }
}

