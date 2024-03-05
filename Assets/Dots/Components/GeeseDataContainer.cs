using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class GeeseDataContainer : IComponentData
{
    public List<GeeseData> geese;
}

public struct GeeseData
{
    public Entity prefab;
    public float moveSpeed;
    public Color color;
}
