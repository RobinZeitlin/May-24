using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BlackMode : MonoBehaviour
{
    public DarkModeType _darkModeType;

    public List<GameObject> colorToChange = new List<GameObject>();
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI tipText;

    public void ChangeMode()
    {
        if(_darkModeType == DarkModeType.DarkMode)
        {
            _darkModeType = DarkModeType.WhiteMode;
            ChangeDarkMode(DarkModeType.WhiteMode);
        }
        else
        {
            _darkModeType = DarkModeType.DarkMode;
            ChangeDarkMode(DarkModeType.DarkMode);
        }
    }

    public void ChangeDarkMode(DarkModeType colorMode)
    {
        switch (colorMode)
        {
            case DarkModeType.DarkMode:
                ChangeColour(Color.black);
                break;

            case DarkModeType.WhiteMode:
                ChangeColour(Color.white);
                break;
        }
    }

    public void ChangeColour(Color _color)
    {
        foreach (var item in colorToChange)
        {
            item.GetComponent<Image>().color = _color;
        }

        levelText.color = _color;
        tipText.color = _color;
    }
}

public enum DarkModeType
{
    DarkMode,
    WhiteMode
}
