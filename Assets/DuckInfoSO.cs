using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DuckInfo", menuName = "DuckInfo")]
public class DuckInfoSO : ScriptableObject
{
    public List<GameObject> hats = new List<GameObject>();
}
