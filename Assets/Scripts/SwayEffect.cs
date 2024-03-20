using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayEffect : MonoBehaviour
{
    public float intensity = 0.5f;
    public float smooth = 5f;
    private Quaternion originRotation;

    private void Start()
    {
        originRotation = transform.localRotation;
    }

    private void Update()
    {
        // Calculate movement sway
        float movementX = -Input.GetAxis("Mouse X") * intensity;
        float movementY = -Input.GetAxis("Mouse Y") * intensity;
        Quaternion finalRotation = Quaternion.Euler(movementY, movementX, 0) * originRotation;

        // Apply the sway effect
        transform.localRotation = Quaternion.Lerp(transform.localRotation, finalRotation, Time.deltaTime * smooth);
    }
}
