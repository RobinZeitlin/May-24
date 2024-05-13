using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public partial struct TriggerSystem : ISystem
{
    private void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;

        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

        var deltaTime = SystemAPI.Time.DeltaTime;
        var entitiesWithGoouseTouched = entityManager.GetAllEntities(Allocator.Temp);

        foreach (var e in entitiesWithGoouseTouched)
        {
            if (entityManager.HasComponent<GoouseTouched>(e))
            {
                var component = entityManager.GetComponentData<GoouseTouched>(e);
                component.removeTimer -= deltaTime;

                if (component.removeTimer <= 0)
                {
                    //Time to remove the component
                    entityManager.RemoveComponent<GoouseTouched>(e);
                }
                else
                {
                    //Update the component data if it's not time to remove
                    entityManager.SetComponentData(e, component);
                }
            }
        }


        foreach (Entity entity in entities)
        {
            if (entityManager.HasComponent<TriggerComponent>(entity))
            {
                RefRW<LocalToWorld> triggerTransform = SystemAPI.GetComponentRW<LocalToWorld>(entity);
                RefRO<TriggerComponent> triggerComponent = SystemAPI.GetComponentRO<TriggerComponent>(entity);

                float size = triggerComponent.ValueRO.size;

                triggerTransform.ValueRW.Value.c0 = new Unity.Mathematics.float4(size, 0, 0, 0);
                triggerTransform.ValueRW.Value.c1 = new Unity.Mathematics.float4(0, size, 0, 0);
                triggerTransform.ValueRW.Value.c2 = new Unity.Mathematics.float4(0, 0, size, 0);

                PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

                NativeList<ColliderCastHit> hits = new NativeList<ColliderCastHit>(Allocator.Temp);

                physicsWorld.SphereCastAll(triggerTransform.ValueRO.Position, triggerComponent.ValueRO.size / 2,
                    float3.zero, 1, ref hits, CollisionFilter.Default);

                foreach(ColliderCastHit hit in hits)
                {

                    if (!entityManager.HasComponent<CollisionBlock>(hit.Entity))
                    {
                        if (!entityManager.HasComponent<GoouseTouched>(hit.Entity))
                        {
                            entityManager.AddComponent<GoouseTouched>(hit.Entity);
                        }

                        var gouseTouched = new GoouseTouched
                        {
                            originalScale = 1f,
                            endScale = 1.1f,
                            duration = 0.1f,
                            time = 0f,
                            removeTimer = 0.38f,
                            scalingUp = false,
                        };

                        entityManager.SetComponentData(hit.Entity, gouseTouched);
                        continue;
                    }

                }

                hits.Dispose();
            }
        }

        entities.Dispose();
    }
}
