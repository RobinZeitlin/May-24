using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public partial struct TriggerSystem : ISystem
{
    private void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;

        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

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
                    Debug.Log(hits.Length);

                    if (entityManager.HasComponent<CollisionBlock>(hit.Entity))
                    {
                        if (!entityManager.HasComponent<GoouseTouched>(entity))
                        {
                            entityManager.AddComponent<GoouseTouched>(entity);
                        }

                        var gouseTouched = new GoouseTouched
                        {
                            endScale = 1.5f,
                            originalScale = 1f,
                            duration = 1f,
                            time = 0f,
                            scalingUp = false,
                        };

                        entityManager.SetComponentData(entity, gouseTouched);

                        GoouseTouched componentAfterSet = entityManager.GetComponentData<GoouseTouched>(entity);

                        if (!Equals(hit.Entity, entity))
                        {
                            Debug.Log($"After setting: EndScale={componentAfterSet.endScale}, OriginalScale={componentAfterSet.originalScale}, Duration={componentAfterSet.duration}, Time={componentAfterSet.time}, ScalingUp={componentAfterSet.scalingUp}");
                        }

                        Debug.Log(Equals(hit.Entity, entity) ? hit.Entity : "Trigger");

                        continue;
                    }

                    Debug.Log("Im not a goose!");
                }
            }
        }

        entities.Dispose();
    }
}
