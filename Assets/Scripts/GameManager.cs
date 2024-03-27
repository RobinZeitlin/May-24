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
        LoadSaveLevel();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(this);
    }

    public void LoadSaveLevel()
    {
        Level = PlayerPrefs.GetInt("Level", 1);
    }

    public void Update()
    {
        levelTextDisplay.text = "Level " + Level;
    }

}
