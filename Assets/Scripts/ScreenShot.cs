using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ScreenCapture.CaptureScreenshot(string.Format("Assets/Screenshots/{0}.png", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));

            print("Took a screenshot!");
        } 
    }
}
