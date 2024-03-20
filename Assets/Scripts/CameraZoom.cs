using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    float zoomScale;

    float minZoom = 30;
    float maxZoom = 120;

    private float targetZoom;

    public void Start()
    {
        zoomScale = Camera.main.fieldOfView;
    }

    public void Update()
    {
        CameraZoomFunc();
    }

    public void CameraZoomFunc()
    {
        zoomScale = Mathf.Clamp(zoomScale, minZoom, maxZoom);

        targetZoom = zoomScale;

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetZoom, Time.deltaTime * 10);

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && zoomScale > minZoom)
        {
            zoomScale -= 1;
        } 
        else if(Input.GetAxis("Mouse ScrollWheel") < 0 && zoomScale < maxZoom)
        {
            zoomScale += 1;
        }
    }
}
