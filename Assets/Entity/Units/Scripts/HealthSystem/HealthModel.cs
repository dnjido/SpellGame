using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public interface IDamage
{
    public void ApplyDamage(float count);
    public void Healing(float count);
    public void Death();
}

public interface IHealthEvents
{
    public delegate void DamageDelegate();
    public event DamageDelegate DamageEvent;

    public delegate void DeathDelegate();
    public event DeathDelegate DeathEvent;

    public (float, float) GetHealth();
}

public class HealthSystemModel : IDamage, IHealthEvents
{
    private GameObject _gameObject;

    private float _maxHealth, _armor;
    private float _currentHealth;

    private float _barierCount => BarierCount();

    public event IHealthEvents.DamageDelegate DamageEvent;
    public event IHealthEvents.DeathDelegate DeathEvent;

    public float percent => _currentHealth / _maxHealth;

    public HealthSystemModel(float maxHealth, float armor, GameObject gameObject)
    {
        _gameObject = gameObject;
        (_currentHealth, _maxHealth) = (maxHealth, maxHealth);
        _armor = armor;
    }

    public (float, float) GetHealth() => (_currentHealth, percent);

    private float BarierCount()
    {
        try { return _gameObject.GetComponent<IEffectAdapter>().IGetGroupEffects.SummEffectsOfType(SpellType.ReduceDamage); }
        catch { return 0; }
    }

    public void ApplyDamage(float count)
    {
        float reduce = _armor / 100 * count;
        _currentHealth -= Mathf.Clamp(count - reduce - _barierCount, 0, int.MaxValue);

        DamageEvent?.Invoke();

        if (percent <= 0) Death();
    }

    public void Healing(float count)
    {
        _currentHealth += count;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        DamageEvent?.Invoke();
    }

    public void Death()
    {
        DeathEvent?.Invoke();
        GameObject.Destroy(_gameObject);
    }
}