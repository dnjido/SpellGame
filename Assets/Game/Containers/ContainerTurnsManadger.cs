using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnsManadger
{
    public void NextTurn();
}

public class ContainerTurnsManadger : MonoBehaviour, ITurnsManadger
{
    [SerializeField] private GameObject[] _unitsList;
    [SerializeField] private string _spellPanelName;

    private TurnsManadger _manadger;
    // Start is called before the first frame update
    void Awake()
    {
        _manadger = new TurnsManadger(_unitsList, _spellPanelName);
    }

    void Start()
    {
        _manadger.SetUnit();
    }

    public void NextTurn() => _manadger.NextTurn();
}
