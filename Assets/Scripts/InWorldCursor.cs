using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldCursor : MonoBehaviour
{
    [SerializeField] private LayerMask layer;

    [SerializeField] private float speed;
    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;

            Vector3 directionToCamera = (Camera.main.transform.position - hitPoint).normalized;

            transform.position = Vector3.Slerp(transform.position, hitPoint + directionToCamera * 5, speed * Time.deltaTime);
        }
    }
}
