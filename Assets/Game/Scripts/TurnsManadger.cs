using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TurnsManadger
{
    private GameObject[] _unitsList;

    private int _id;
    private ISpellButtonsPanel _spellPanel;

    private GameObject currentUnit => _unitsList[_id];

    public TurnsManadger(GameObject[] unitsList, string spellPanelName)
    {
        _unitsList = unitsList;
        _spellPanel = GameObject.Find(spellPanelName).GetComponent<ISpellButtonsPanel>();
    }

    public void NextTurn() => NextUnit();

    private void NextUnit()
    {
        UnitRemove();

        _id++;
        if (_id >= _unitsList.Length) NewRound();

        SetUnit();
    }

    private void FillSpellPanel()
    {
        List<SpellsCooldown> spells = currentUnit.GetComponent<IUnitSpellsAdapter>().ISpellsController.GetSpellList();
        _spellPanel.CreateButtons(spells.ToArray(), currentUnit);
    }

    private void UnitRemove()
    {
        if (_spellPanel != null) _spellPanel.RemoveButtons();
        currentUnit.GetComponent<IMarkSpawner>().MarkRemove();
    }

    public void SetUnit()
    {
        if (currentUnit.GetComponent<IAIAdapter>().AIModel.IsAI())
        {
            _spellPanel.SetActive(false);
            currentUnit.GetComponent<IAIAdapter>().AIModel.AIAction();
        }
            
        else
        {
            _spellPanel.SetActive(true);
            FillSpellPanel();
            currentUnit.GetComponent<IMarkSpawner>().MarkCreate();
        }
    }

    private void NewRound()
    {
        _id = 0;
        foreach (GameObject unit in _unitsList)
        {
            unit.GetComponent<ITurnAdapter>().ITurnController.TurnOver();
        }

        GameObject[] effectButtons = GameObject.FindGameObjectsWithTag("EffectIcon");

        foreach (var button in effectButtons)
        {
            button.GetComponent<IEffectIcon>().UpdateEffect();
        }
    }
}
