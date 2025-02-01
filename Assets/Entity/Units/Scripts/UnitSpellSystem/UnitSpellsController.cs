using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IUnitSpellsAdapter
{
    public ISpellsController ISpellsController { get; set; }
}

public class UnitSpellsController : MonoBehaviour, IUnitSpellsAdapter
{
    private UnitSpellsModel _spellModel;

    public ISpellsController ISpellsController { get; set; }

    [SerializeField] private string[] _spellList;
    [SerializeField] SpellScriptableObject _spells;

    // Start is called before the first frame update
    private void Awake()
    {
        _spellModel = new UnitSpellsModel(_spellList, _spells, gameObject);
        ISpellsController = _spellModel;
    }
}
