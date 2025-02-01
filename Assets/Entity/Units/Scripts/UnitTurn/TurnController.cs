using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnAdapter
{
    public ITurnController ITurnController { get; set; }
}

public class TurnController : MonoBehaviour, ITurnAdapter
{
    private TurnModel _TurnModel;

    [SerializeField] private int _initTurn = 1;

    public ITurnController ITurnController { get; set; }

    private void Awake()
    {
        _TurnModel = new TurnModel(_initTurn, gameObject);
        ITurnController = _TurnModel;
    }
}