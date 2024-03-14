using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using ECS;
using UnityEngine;
using System;
using Random = Unity.Mathematics.Random;
using Unity.VisualScripting.FullSerializer;

[BurstCompile]

public partial struct EntitySystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Random random = new Random((uint)UnityEngine.Random.Range(1, 100000));
        EntityManager entityManager = state.EntityManager;

        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

        foreach (Entity entity in entities)
        {
            if (entityManager.HasComponent<EntityComponent>(entity))
            {
                EntityComponent entityRef = entityManager.GetComponentData<EntityComponent>(entity);
                LocalTransform localTransform = entityManager.GetComponentData<LocalTransform>(entity);

                float3 moveDirection = localTransform.Forward() * SystemAPI.Time.DeltaTime * entityRef.moveSpeed;

                Quaternion rotation = Quaternion.Euler(0, entityRef.rotationAngle * entityRef.directionValue * SystemAPI.Time.DeltaTime, 0);
                localTransform.Rotation = localTransform.Rotation * rotation;

                localTransform.Position += moveDirection * 0.1f;

                double sineWaveOffset = math.sin(SystemAPI.Time.ElapsedTime * 20) * (0.001f + (entityRef.moveSpeed * 0.0005));

                localTransform.Position.y += (float)sineWaveOffset;

                entityManager.SetComponentData<LocalTransform>(entity, localTransform);

            }

            if (entityManager.HasComponent<ScaleEffect>(entity))
            {
                ScaleEffect scaleEffect = entityManager.GetComponentData<ScaleEffect>(entity);
                if (scaleEffect.timeElapsed < scaleEffect.duration)
                {
                    float progress = scaleEffect.timeElapsed / scaleEffect.duration;
                    float currentScale = math.lerp(scaleEffect.startScale, scaleEffect.endScale, progress);

                    LocalTransform scale = entityManager.GetComponentData<LocalTransform>(entity);
                    scale.Scale = currentScale;

                    entityManager.SetComponentData(entity, scale);

                    scaleEffect.timeElapsed += SystemAPI.Time.DeltaTime;
                    entityManager.SetComponentData(entity, scaleEffect);
                }
            }
        }

        entities.Dispose();
    }
}