using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injection
{
    private const string _gameManadgerName = "GameManadger";
    private readonly GameObject _gameManadger;

    public Injection()
    {
        _gameManadger = GameObject.Find(_gameManadgerName);
    }

    public T Inject<T>() => _gameManadger.GetComponentInChildren<T>();
}
