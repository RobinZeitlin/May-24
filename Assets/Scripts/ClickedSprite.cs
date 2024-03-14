using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedSprite : MonoBehaviour
{
    public Sprite notClicked;
    public Sprite Clicked;
    public void ChangeSprite()
    {
        Image image = GetComponent<Image>();

        if(image.sprite == notClicked)
        {
            image.sprite = Clicked;
        }
        else
        {
            image.sprite = notClicked;
        }
    }
}
