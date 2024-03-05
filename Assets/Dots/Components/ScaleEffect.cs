using Unity.Entities;
using Unity.Mathematics;

public struct ScaleEffect : IComponentData
{
    public float startScale;
    public float endScale;
    public float duration;
    public float timeElapsed;
}