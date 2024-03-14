using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Random = Unity.Mathematics.Random;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;
using System.Linq;
using System.Threading.Tasks;

namespace ECS
{
    [BurstCompile]
    public partial struct EntitySpawnerSystem : ISystem
    {
        public static EntitySpawnerSystem instance;
        public void Initialize(ref SystemState state)
        {
            instance = this;
        }

        public static int EntityCount;

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (EntityCount < 10 + (25 * GameManager.instance.Level))
            {
                SpawnEntity(ref state);
                EntityCount++;
            }
        }

        [BurstCompile]
        public void ClearAllEntitys(EntityManager entityManager)
        {
            if(entityManager == null)
            {
                return;
            }
            EntityQuery query = entityManager.CreateEntityQuery(typeof(LocalTransform));

            using (NativeArray<Entity> entities = query.ToEntityArray(Allocator.Temp))
            {
                entityManager.DestroyEntity(entities);
            }
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
                    Rotation = quaternion.Euler(0, random.NextFloat(0, 360), 0),
                });

                //ScaleEffect Initializer
                ecb.AddComponent(newEntity, new ScaleEffect
                {
                    startScale = 0,
                    endScale = random.NextFloat(0.75f, 1.25f),
                    duration = random.NextFloat(0.5f,1f),
                    timeElapsed = 0,
                });

                //EntityComponent Initializer
                if(random.NextBool())
                ecb.AddComponent(newEntity, new EntityComponent
                {
                    moveDirection = math.forward(),
                    moveSpeed = random.NextFloat(5,25),
                    directionValue = RandomSign(random),
                    rotationAngle = random.NextFloat(15, 100),
            });

                spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnInterval;

                ecb.Playback(state.EntityManager);
            }

        }
        public Vector3 GetRandomPosition(Random _random)
        {
            return new Vector3(_random.NextFloat(-15, 15), 0, _random.NextFloat(-15, 15));
        }

        static int RandomSign(Random _random)
        {
            int randomNumber = _random.NextInt(2);

            return randomNumber == 0 ? -1 : 1;
        }
    }
}
