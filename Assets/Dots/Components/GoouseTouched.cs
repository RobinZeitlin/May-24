using Unity.Entities;

public struct GoouseTouched : IComponentData
{
    public float originalScale;
    public float endScale;
    public float duration;
    public float time;
    public bool scalingUp;
}
