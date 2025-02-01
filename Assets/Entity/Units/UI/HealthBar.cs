using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface ISetHealthBar
{
    public void SetBar(IHealthAdapter healthEvents);
}

public interface IAddEffectOnBar
{
    public void AddEffect(UnitEffect effect);
}

public class HealthBar : MonoBehaviour, ISetHealthBar, IAddEffectOnBar
{
    [SerializeField] private GameObject _fill, _counter, _effectIconPrefab, _effectPanel;

    private Image _fillImage => _fill.GetComponent<Image>();
    private TMP_Text _counterText => _counter.GetComponent<TMP_Text>();

    private IHealthAdapter _healthEvents;

    public void SetBar(IHealthAdapter healthEvents) //IHealthEvents.DeathEvent
    {
        _healthEvents = healthEvents;
        OnEnable();
    }

    private void Change()
    {
        var count = _healthEvents.IHealthEvents.GetHealth();
        _fillImage.fillAmount = count.Item2;
        _counterText.text = ((int)count.Item1).ToString();
    }

    public void AddEffect(UnitEffect effect)
    {
        GameObject icon = Instantiate(_effectIconPrefab, _effectPanel.transform);
        icon.transform.localScale = Vector3.one * 0.05f;
        icon.GetComponent<IEffectIcon>().Create(effect);
    }

    private void OnEnable()
    {
        if(_healthEvents == null) return;
        _healthEvents.IHealthEvents.DamageEvent += Change;
        Change();
    }

    private void OnDisable()
    {
        if(_healthEvents == null) return;
        _healthEvents.IHealthEvents.DamageEvent -= Change;
    }
}
