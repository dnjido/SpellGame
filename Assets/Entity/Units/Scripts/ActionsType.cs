using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseAction
{
    protected readonly GameObject _target;
    protected readonly SpellStatus _spell;

    public BaseAction(GameObject target, SpellStatus spell)
    {
        _target = target;
        _spell = spell;
        Action();
    }

    protected abstract void Action();
}

public class DamageAction : BaseAction
{
    public DamageAction(GameObject target, SpellStatus spell) : base(target, spell) { }

    protected override void Action()
    {
        float damage = _spell.effects.Where(e => e.type == 0).Sum(t => t.count);
        _target.GetComponent<IHealthAdapter>().IDamage.ApplyDamage(damage);
    }
}

public class EffectAction : BaseAction
{
    public EffectAction(GameObject target, SpellStatus spell) : base(target, spell) { }

    protected override void Action()
    {
        _target.GetComponent<IEffectAdapter>()?.IEffect.ApplyEffect(_spell);
    }
}

public class CleansingAction : BaseAction
{
    public CleansingAction(GameObject target, SpellStatus spell) : base(target, spell) { }

    protected override void Action()
    {
        _target.GetComponent<IEffectAdapter>()?.IGetGroupEffects.RemoveEffectsOfType(SpellType.Burning);
    }
}
