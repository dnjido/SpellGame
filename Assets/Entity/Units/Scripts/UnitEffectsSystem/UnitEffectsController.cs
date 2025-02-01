using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectAdapter
{
    public IEffect IEffect { get; set; }
    public IGetGroupEffects IGetGroupEffects { get; set; }
}

public class UnitEffectsController : MonoBehaviour, IEffectAdapter
{
    private UnitEffectsModel _effectModel;

    public IEffect IEffect { get; set; }
    public IGetGroupEffects IGetGroupEffects { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        _effectModel = new UnitEffectsModel(gameObject);
        IEffect = _effectModel;
        IGetGroupEffects = _effectModel;
    }
}
