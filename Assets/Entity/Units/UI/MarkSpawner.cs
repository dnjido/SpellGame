using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarkSpawner
{
    public void MarkCreate();
    public void MarkRemove();
}

public class MarkSpawner : MonoBehaviour, IMarkSpawner
{
    [SerializeField] private GameObject _currentMarkPrefab;
    [SerializeField] private GameObject _markPoint;
    private GameObject _currentMark;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarkCreate()
    {
        _currentMark = Instantiate(_currentMarkPrefab, _markPoint.transform.position, _currentMarkPrefab.transform.rotation);
        _currentMark.transform.localScale = Vector3.one * 0.1f;
    }

    public void MarkRemove()
    {
        Destroy(_currentMark);
    }
}
