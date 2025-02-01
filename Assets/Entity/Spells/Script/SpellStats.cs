using System;
using UnityEngine;

[Serializable]
public class SpellStatus
{
    [SerializeField] public string nameID;
    [SerializeField] public string name;
    [SerializeField] public Sprite icon;
    [SerializeField] public SpellEffects[] effects;
    [SerializeField] public TargetType target;
    [SerializeField] public int duration, reload;
}

[Serializable]
public struct SpellEffects
{
    [SerializeField] public SpellType type;
    [SerializeField] public float count;
}

public enum SpellType
{
    Damage = 0,
    ReduceDamage = 1,
    Regeneration = 2,
    Burning = 3,
    ClearStatus = 4,
}

public enum TargetType
{
    Self = 0,
    Enemy = 1,
    Ally = 2,
}
