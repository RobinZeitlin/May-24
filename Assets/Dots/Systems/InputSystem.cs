using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class InputSystem : SystemBase
{
    public Controls controls;
    protected override void OnCreate()
    {
        if(!SystemAPI.TryGetSingleton<InputComponent>(out InputComponent input))
        {
            EntityManager.CreateEntity(typeof(InputComponent));
        }

        controls = new Controls();
        controls.Enable();
    }

    protected override void OnUpdate()
    {
        Vector2 mousePos = controls.ActionMap.MousePosition.ReadValue<Vector2>();

        SystemAPI.SetSingleton(new InputComponent
        {
            mousePos = mousePos
        });
    }
}
