using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ISpellsController
{
    public List<SpellsCooldown> GetSpellList();
    public void TurnLeft();
}

public class UnitSpellsModel : ISpellsController
{
    private GameObject _gameObject;

    private string[] _spellList;
    private SpellScriptableObject _spells;
    private List<SpellsCooldown> _spellsCooldowns = new List<SpellsCooldown>();

    public UnitSpellsModel(string[] spellList, SpellScriptableObject spells, GameObject gameObject) 
    {
        _gameObject = gameObject;
        _spellList = spellList;
        _spells = spells;
        CreateSpells();
    }

    public List<SpellsCooldown> GetSpellList() => _spellsCooldowns;

    private void CreateSpells()
    {
        foreach (string spell in _spellList)
        {
            SpellStatus currentSpells = _spells.GetSpell(spell);
            SpellsCooldown currentCooldown = new SpellsCooldown(currentSpells);
            _spellsCooldowns.Add(currentCooldown);
            currentCooldown.UseEvent += Use;
        }
    }

    public void TurnLeft()
    {
        foreach (SpellsCooldown spell in _spellsCooldowns)
        {
            spell.TurnRemove();
        }
    }

    public void Use()
    {
        _gameObject.GetComponent<ITurnAdapter>().ITurnController.TurnLeft();
    }
}

public class SpellsCooldown
{
    public readonly SpellStatus _spell;
    public int _cooldown { get; private set; }

    public SpellsCooldown(SpellStatus spell)
    {
        _spell = spell;
    }

    public delegate void UseDelegate();
    public event UseDelegate UseEvent;

    public void Use()
    {
        if (!TurnCheck()) return;
        _cooldown = _spell.reload;
        UseEvent?.Invoke();
    }

    public void TurnRemove() => _cooldown--;

    public bool TurnCheck() => _cooldown <= 0;
}
