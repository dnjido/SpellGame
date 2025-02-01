using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IEffectIcon
{
    public void Create(UnitEffect effect);
    public void UpdateEffect();
}

public class EffectIcon : MonoBehaviour, IEffectIcon
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _reloardBar, _counter, _panelCounter;

    private Image bar => _reloardBar.GetComponent<Image>();
    private TMP_Text textCounter => _counter.GetComponent<TMP_Text>();

    private UnitEffect _effect;
    private SpellStatus _spell => _effect._spell;
    private int _duration => _effect._turnLeft;

    // Update is called once per frame
    public void Create(UnitEffect effect)
    {
        _effect = effect;
        _icon.sprite = _spell.icon;
        _effect.EffectClearEvent += Clear;
        UpdateEffect();
    }

    public void UpdateEffect()
    {
        bar.fillAmount = (float)_duration / _spell.duration;
        if (_effect._turnLeft <= 0) Clear();
        textCounter.text = _duration.ToString();
    }

    private void Clear()
    {
        _effect.EffectClearEvent -= Clear;
        if (_duration <= 0) _panelCounter.SetActive(false);
        Destroy(gameObject);
    }
}
