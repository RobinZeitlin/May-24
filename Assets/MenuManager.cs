using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Settings")]
    public int waitDelay = 100;

    [Header("Content")]
    public Transform menuParent;
    private List<GameObject> Content = new List<GameObject>();

    private bool menuOpen = false;

    private void OnEnable()
    {
        for (int i = 0; i < menuParent.childCount; i++)
        {
            Content.Add(menuParent.GetChild(i).gameObject);
            menuParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ShowContent()
    {
        if(!menuOpen)
            DisplayContent();
        else
            HideContent();
    }

    public async void DisplayContent()
    {
        for (int i = 0; i < Content.Count; i++)
        {
            await Task.Delay(waitDelay);
            Content[i].SetActive(true);
        }

        menuOpen = true;
    }

    public void HideContent()
    {
        for (int i = 0; i < Content.Count; i++)
        {
            Content[i].SetActive(false);
        }
        menuOpen = false;
    }
}
