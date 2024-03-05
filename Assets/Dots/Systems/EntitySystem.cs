using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using ECS;
using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;

[BurstCompile]

public partial struct EntitySystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;

        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

        foreach (Entity entity in entities)
        {
            if (entityManager.HasComponent<EntityComponent>(entity))
            {
                EntityComponent entityRef = entityManager.GetComponentData<EntityComponent>(entity);
                LocalTransform localTransform = entityManager.GetComponentData<LocalTransform>(entity);

                float3 moveDirection = entityRef.moveDirection * SystemAPI.Time.DeltaTime * entityRef.moveSpeed;

                localTransform.Position = localTransform.Position + moveDirection * 0.6f;

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