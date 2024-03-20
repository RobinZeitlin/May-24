using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public float Intensity = 5f;
    public float Speed = 1f;

    private float originalY;

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        float newY = originalY + Mathf.Sin(Time.time * Speed) * Intensity;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
