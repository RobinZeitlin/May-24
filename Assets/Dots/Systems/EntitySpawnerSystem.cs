using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Random = Unity.Mathematics.Random;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;

namespace ECS
{
    [BurstCompile]
    public partial struct EntitySpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            SpawnEntity(ref state);
        }

        [BurstCompile]
        public void SpawnEntity(ref SystemState state)
        {
            Random random = new Random((uint)UnityEngine.Random.Range(1, 100000));

            if (!SystemAPI.TryGetSingletonEntity<EntitySpawner>(out Entity spawnerEntity))
            {
                return;
            }

            RefRW<EntitySpawner> spawner = SystemAPI.GetComponentRW<EntitySpawner>(spawnerEntity);

            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

            if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = ecb.Instantiate(spawner.ValueRO.prefab);
                Vector3 randomPosition = spawner.ValueRO.spawnPos + GetRandomPosition(random);

                //Transform Initializer
                ecb.SetComponent(newEntity, new LocalTransform 
                { 
                    Position = new float3(randomPosition.x, randomPosition.y, randomPosition.z),
                });

                //ScaleEffect Initializer
                ecb.AddComponent(newEntity, new ScaleEffect
                {
                    startScale = 0,
                    endScale = random.NextFloat(0.75f, 1.25f),
                    duration = random.NextFloat(0.5f,1f),
                    timeElapsed = 0
                });

                //EntityComponent Initializer
                ecb.AddComponent(newEntity, new EntityComponent
                {
                    moveDirection = math.forward(),
                    moveSpeed = 10,
                });

                spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnInterval;

                ecb.Playback(state.EntityManager);
            }
        }
        public Vector3 GetRandomPosition(Random random)
        {
            return new Vector3(random.NextFloat(-30, 30), 0, random.NextFloat(-30, 30));
        }
    }
}
