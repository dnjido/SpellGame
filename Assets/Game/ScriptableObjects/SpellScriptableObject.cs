using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellScriptableObject", menuName = "ScriptableObjects/Spells")]
public class SpellScriptableObject : ScriptableObject
{
    [SerializeField] private SpellStatus[] _spells;

    public SpellStatus GetSpell(int id) => _spells[id];
    public SpellStatus GetSpell(string nameID) => _spells.FirstOrDefault(e => e.nameID == nameID);

}
