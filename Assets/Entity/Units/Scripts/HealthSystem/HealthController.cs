using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthAdapter
{
    public IDamage IDamage { get; set; }
    public IHealthEvents IHealthEvents { get; set; }
}

public class HealthController : MonoBehaviour, IHealthAdapter
{
    private HealthSystemModel _healthModel;

    public IDamage IDamage { get; set; }
    public IHealthEvents IHealthEvents { get; set; }

    [SerializeField] private float _health, _armor;

    // Start is called before the first frame update
    private void Awake()
    {
        _healthModel = new HealthSystemModel(_health, _armor, gameObject);
        IDamage = _healthModel;
        IHealthEvents = _healthModel;
    }
}
