using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;
using System;
using System.Collections.Generic;

namespace ECS
{
    public class EntitySpawnerAuthoring : MonoBehaviour
    {
        public GameObject prefab;
        public float spawnRate;
        public float spawnCount;
    }

    class EntitySpawnerBaker : Baker<EntitySpawnerAuthoring>
    {
        private Random random;

        public override void Bake(EntitySpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            random = new Random((uint)UnityEngine.Random.Range(1, 100000));

            AddComponent(entity, new EntitySpawner
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                spawnPos = authoring.transform.position,
                nextSpawnTime = 0,
                spawnInterval = authoring.spawnRate,
                spawnCount = authoring.spawnCount,
            });
        }
    }
}
