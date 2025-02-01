using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerGameStatus : MonoBehaviour
{
    [SerializeField]private GameStatus _gameStatus;
    void Awake()
    {
        _gameStatus = new GameStatus(this);
    }

    void Start() => _gameStatus.GetAllUnits();

    // Update is called once per frame
    public void ResetGame() => _gameStatus.ResetGame();
}
