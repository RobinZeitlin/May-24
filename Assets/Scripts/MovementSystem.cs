using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float dragSpeed = 22;

    private float currentDragSpeed = 22;
    private Vector3 dragOrigin;

    bool moveToOrigin = false;
    float distanceToOrigin;

    void Update()
    {
        Vector3 offset = new Vector3(8f, transform.position.y, 0);
        Vector3 origin = Vector3.zero + offset;

        if (Input.GetKeyDown(KeyCode.F))
        {
            distanceToOrigin = Vector3.Distance(transform.position, origin);
            moveToOrigin = !moveToOrigin;
        }

        if(moveToOrigin)
        {
            transform.position = Vector3.MoveTowards(transform.position, origin, distanceToOrigin * Time.deltaTime);

            if(Vector3.Distance(transform.position, origin) < 0.1f)
                moveToOrigin = false;

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.y * currentDragSpeed, 0, -pos.x * currentDragSpeed);

        transform.Translate(move, Space.World);
        dragOrigin = Input.mousePosition;

        currentDragSpeed = dragSpeed * (Camera.main.fieldOfView / 60);
    }
}
