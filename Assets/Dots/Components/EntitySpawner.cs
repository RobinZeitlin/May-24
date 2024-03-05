using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct EntitySpawner : IComponentData
{
    public Entity prefab;
    public Vector3 spawnPos;

    public float nextSpawnTime;
    public float spawnInterval;
}
