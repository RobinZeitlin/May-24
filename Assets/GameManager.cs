using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Level = 1;

    public TextMeshProUGUI levelTextDisplay;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        levelTextDisplay.text = "Level: " + Level;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        Debug.Log("Level: " + Level);
    }
}
