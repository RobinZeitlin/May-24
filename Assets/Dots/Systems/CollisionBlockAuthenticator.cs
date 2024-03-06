using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CollisionBlockAuthenticator : MonoBehaviour
{
    public class TriggerBaker : Baker<TriggerAuthorin>
    {
        public override void Bake(TriggerAuthorin authoring)
        {
            Entity triggerAuthoring = GetEntity(TransformUsageFlags.None);

            AddComponent(triggerAuthoring, new CollisionBlock{});
            /*AddComponent(triggerAuthoring, new GoouseTouched
            {
                endScale = 1.5f,
                originalScale = 1f,
                duration = 1f,
                time = 0f,
                scalingUp = false,
            });*/
        }
    }
}
