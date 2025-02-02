using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnController
{
    public void TurnLeft();
    public void TurnReset();
    public void TurnOver();
    public int GetTurns();
}

public class TurnModel : ITurnController
{
    private GameObject _gameObject;

    private int _initTurn;
    private int _turnCount;

    private ITurnsManadger _turnsManadger;
    private MonoBehaviour _monoBehaviour;

    public TurnModel(int initTurn, GameObject gameObject, MonoBehaviour monoBehaviour) 
    {
        _monoBehaviour = monoBehaviour;
        (_initTurn, _turnCount) = (initTurn, initTurn);
        _gameObject = gameObject;
        _turnsManadger = new Injection().Inject<ITurnsManadger>();
    }

    public int GetTurns() => _turnCount;

    public void TurnLeft()
    {
        _turnCount--;
        if (_turnCount <= 0) _monoBehaviour.StartCoroutine(NextTurnDelay());
    }

    IEnumerator NextTurnDelay()
    {
        yield return new WaitForSeconds(.1f);
        _turnsManadger.NextTurn();
    }

    public void TurnReset() => _turnCount = _initTurn;

    public void TurnOver()
    {
        TurnReset();
        _gameObject.GetComponent<IHealthAdapter>()?.IDamage.Healing(GetSummEffects(SpellType.Regeneration));
        _gameObject.GetComponent<IHealthAdapter>()?.IDamage.ApplyDamage(GetSummEffects(SpellType.Burning));
        _gameObject.GetComponent<IEffectAdapter>()?.IEffect.EffectTurnLeft();
        _gameObject.GetComponent<IUnitSpellsAdapter>()?.ISpellsController.TurnLeft();
    }

    private float GetSummEffects(SpellType effect)
    {
        return _gameObject.GetComponent<IEffectAdapter>().IGetGroupEffects.SummEffectsOfType(effect);
    }
}
