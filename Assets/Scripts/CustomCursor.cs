using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    
    private Vector2 cursorHotspot = Vector2.zero;

    private void Start()
    {
        if(cursorTexture == null)
        {
            Cursor.visible = false;
            return;
        }

        Debug.Log("Setting custom cursor");

        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
}
