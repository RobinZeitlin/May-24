using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

[UpdateInGroup(typeof(TransformSystemGroup))]
public partial class FollowMouse : SystemBase
{
    protected override void OnUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Vector3 worldPositon = hit.point;

            Entities.WithAll<TriggerComponent>().ForEach((ref LocalTransform translation) =>
            {
                translation.Position = new float3(worldPositon);
            }).ScheduleParallel();
        }
    }
}
