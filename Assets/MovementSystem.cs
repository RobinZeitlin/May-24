using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public Controlls controls; // Reference to the generated input controls class

    private Vector2 move;

    private void Awake()
    {
        controls = new Controlls();

        controls.ActionMap.Drag.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.ActionMap.Drag.canceled += ctx => move = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.ActionMap.Enable();
    }

    private void OnDisable()
    {
        controls.ActionMap.Disable();
    }

    private void Update()
    {
        Vector3 delta = new Vector3(move.x, move.y, 0) * Time.deltaTime;
        transform.Translate(delta);
    }
}
