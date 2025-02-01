using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorScriptableObject", menuName = "ScriptableObjects/Colors")]
public class ColorScriptableObject : ScriptableObject
{
    [SerializeField] public Material[] _materials;

}
