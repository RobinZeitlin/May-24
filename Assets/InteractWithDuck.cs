using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractWithDuck : MonoBehaviour
{
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ClickDuck();
        }
    }
    public void ClickDuck()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            if(hit.collider.gameObject.CompareTag("Duck"))
            {
                GameManager.instance.Level++;
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
