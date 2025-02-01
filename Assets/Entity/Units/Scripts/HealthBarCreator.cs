using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class HealthBarCreator : MonoBehaviour
{
    [SerializeField] private GameObject _barPrefab, point;
    public GameObject healthBar;

    public ISetHealthBar healthBarComponent => healthBar.GetComponent<ISetHealthBar>();
    public IAddEffectOnBar addEffectBar => healthBar.GetComponent<IAddEffectOnBar>();

    private void Start() => CreateBar();

    private void CreateBar()
    {
        healthBar = Instantiate(_barPrefab, point.transform);
        healthBar.transform.localScale = Vector3.one * 0.2f;
        healthBarComponent.SetBar(GetComponent<IHealthAdapter>());
    }
}
