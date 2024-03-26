using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBounce : MonoBehaviour
{
    public float frequency = 1f;
    public float magnitude = 0.5f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * frequency) * magnitude;
        transform.localScale = originalScale * scale;
    }
}
