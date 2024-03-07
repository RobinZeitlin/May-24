using Unity.Entities;
using UnityEngine;

public class TriggerAuthorin : MonoBehaviour
{
    public float size;
    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, size / 2);
    }
    public class TriggerBaker : Baker<TriggerAuthorin> 
    {
        public override void Bake(TriggerAuthorin authoring)
        {
            Entity triggerAuthoring = GetEntity(TransformUsageFlags.None);

            AddComponent(triggerAuthoring, new TriggerComponent 
            { 
                size = authoring.size 
            });
        }
    }
}
