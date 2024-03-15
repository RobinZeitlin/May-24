using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    float zoomScale;

    float minZoom = 5;
    float maxZoom = 12;

    public void Start()
    {
        zoomScale = Camera.main.orthographicSize;
    }

    public void Update()
    {
        CameraZoomFunc();
    }

    public void CameraZoomFunc()
    {
        Camera.main.orthographicSize = Mathf.Clamp(zoomScale, minZoom, maxZoom);

        if(Input.GetAxis("Mouse ScrollWheel") > 0 && zoomScale > minZoom)
        {
            zoomScale -= 1;
        } 
        else if(Input.GetAxis("Mouse ScrollWheel") < 0 && zoomScale < maxZoom)
        {
            zoomScale += 1;
        }
    }
}
