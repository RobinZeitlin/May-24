using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DuckInfo", menuName = "DuckInfo")]
public class DuckInfoSO : ScriptableObject
{
    public List<duckInfo> hats = new List<duckInfo>();
}

[System.Serializable]
public class duckInfo
{
    public GameObject hatPrefab;
    public DuckToFind duckToFind;
    public Sprite hatSprite;
}
public enum DuckToFind
{
    None,
    VikingHelmet,
    CowboyHat,
    TopHat,
    Crown,
    Sombrero,
    MinerHat,
    CapBlue,
    CapRed,
    SunHat,
}

