using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ISpellButtonsPanel
{
    public void CreateButtons(SpellsCooldown[] spellList, GameObject caster);
    public void RemoveButtons();
    public void SetActive(bool active);
}

public class SpellPanel : MonoBehaviour, ISpellButtonsPanel
{
    [SerializeField] private GameObject _buttonPrefab;
    private List<GameObject> _buttons = new List<GameObject>();

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void CreateButtons(SpellsCooldown[] spellList, GameObject caster)
    {
        foreach (SpellsCooldown spell in spellList)
        {
            GameObject button = Instantiate(_buttonPrefab, gameObject.transform); 
            button.GetComponent<ISpellButtonSet>()?.ButtonSet(spell, caster);
            _buttons.Add(button);
        }
    }

    public void RemoveButtons()
    {
        foreach (GameObject button in _buttons)
        {
            Destroy(button);
        }
    }
}
