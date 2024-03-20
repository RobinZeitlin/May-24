using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;
using System;

public class MoveRandomly : MonoBehaviour
{
    public float direction;
    public float moveSpeed;
    public float directionValue;
    public float rotationAngle;

    private void Awake()
    {
        Random random = new Random((uint)UnityEngine.Random.Range(1, 100000));

        moveSpeed = random.NextFloat(5, 25);
        directionValue = RandomSign(random);
        rotationAngle = random.NextFloat(15, 100);
    }

    private void Update()
    {
        float3 moveDirection = transform.forward * moveSpeed;

        Quaternion rotation = Quaternion.Euler(0, rotationAngle * directionValue * Time.deltaTime, 0);
        transform.localRotation = transform.localRotation * rotation;

        transform.position += (Vector3)moveDirection * 0.1f * Time.deltaTime;

        double sineWaveOffset = math.sin(20 * (0.001f + (moveSpeed * 0.0005)));

        Vector3 position = transform.position;
        position.y = (float)sineWaveOffset;
        transform.position = position;
    }

    static int RandomSign(Random _random)
    {
        int randomNumber = _random.NextInt(2);

        return randomNumber == 0 ? -1 : 1;
    }
}
