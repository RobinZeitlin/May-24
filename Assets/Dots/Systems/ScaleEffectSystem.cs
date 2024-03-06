using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public partial class ScaleEffectSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var entityManager = this.EntityManager;

        Entities.ForEach((ref GoouseTouched effect, ref LocalTransform scale) =>
        {
            var progress = math.clamp(effect.time / effect.duration, 0f, 1f);
            scale.Scale = math.lerp(effect.originalScale, effect.endScale, progress);

            if (effect.scalingUp)
            {
                if (effect.time >= effect.duration)
                {
                    effect.time = 0f;
                    effect.scalingUp = false;
                    effect.endScale = effect.originalScale;
                    effect.originalScale = scale.Scale;
                }
            }
            else
            {
                if (effect.time >= effect.duration)
                {
                    effect.time = 0f;
                    effect.scalingUp = true;
                    effect.originalScale = effect.endScale;
                    effect.endScale = 2f * effect.originalScale; // Example scaling factor
                }
            }

            effect.time += deltaTime;

        }).ScheduleParallel();
    }
}
