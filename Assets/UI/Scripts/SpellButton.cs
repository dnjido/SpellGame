using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface ISpellButtonSet
{
    public void ButtonSet(SpellsCooldown spell, GameObject caster);
}

public class SpellButton : MonoBehaviour, ISpellButtonSet
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _reloardBar, _counter, _panelCounter;

    private Image bar => _reloardBar.GetComponent<Image>();
    private TMP_Text textCounter => _counter.GetComponent<TMP_Text>();

    private GameObject _caster;
    private ISetSpellAction _playerAction;

    private SpellsCooldown _spellCooldown;
    private SpellStatus _spell => _spellCooldown._spell;
    private int _cooldown => _spellCooldown._cooldown;

    private void Start()
    {
        _playerAction = new Injection().Inject<ISetSpellAction>();
    }

    public void ButtonSet(SpellsCooldown spell, GameObject caster)
    {
        _caster = caster;
        _spellCooldown = spell;
        _icon.sprite = _spell.icon;

        Cooldown();
    }

    private int GetTurns()
    {
        return _caster.GetComponent<ITurnAdapter>().ITurnController.GetTurns();
    }

    public void Click()
    {
        if (GetTurns() <= 0) return;

        if (_spell.target == TargetType.Self)
        {
            _playerAction.ClearSpell();
            ActionSelectot.SelectAction(_caster, _spell);
            SpellUse();
        }
            
        if (_spell.target == TargetType.Enemy)
        {
            _playerAction.SetSpell(_spell, _caster.GetComponent<ITeam>().GetRelantioship());

            ChangeColor(GetComponent<Button>().colors.pressedColor);
            _playerAction.IPlayerAction.PlayerActionEvent += SpellUse;
            _playerAction.IPlayerAction.CancelActionEvent += ActionCancel;
        }
    }

    private void SpellUse()
    {
        _playerAction.IPlayerAction.PlayerActionEvent -= SpellUse;
        _playerAction.IPlayerAction.CancelActionEvent -= ActionCancel;
        _spellCooldown.Use();
        Cooldown();
    }

    private void ActionCancel()
    {
        _playerAction.IPlayerAction.PlayerActionEvent -= SpellUse;
        _playerAction.IPlayerAction.CancelActionEvent -= ActionCancel;

        ChangeColor(Color.white);
    }

    private void ChangeColor(Color color)
    {
        GetComponent<Image>().color = color;
    }

    private void Cooldown()
    {
        if (_spell.reload <= 0) { 
            bar.fillAmount = 0;
            _panelCounter.SetActive(false);
            return;
        }

        if (_cooldown > 0) GetComponent<Button>().interactable = false;
        if (_cooldown <= 0) _panelCounter.SetActive(false);
        bar.fillAmount = (float)_cooldown / _spell.reload;
        textCounter.text = _cooldown.ToString();
    }
}
