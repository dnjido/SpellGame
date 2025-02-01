using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public interface IAIAdapter
{
    public AIModel AIModel { get; set; }
}

public class AIController : MonoBehaviour, IAIAdapter
{
    private AIModel _AIModel;
    [SerializeField]private bool _isAI;
    // Start is called before the first frame update
    public AIModel AIModel { get; set; }

    private void Awake()
    {
        _AIModel = new AIModel(_isAI, gameObject, this);
        AIModel = _AIModel;
    }
}
