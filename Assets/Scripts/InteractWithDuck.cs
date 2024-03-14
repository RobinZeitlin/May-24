using ECS;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractWithDuck : MonoBehaviour
{
    public DuckToFindHandler DuckToFindHandler;
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
                //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

                LevelSystem levelSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<LevelSystem>();
                levelSystem.ChangeLevel(GameManager.instance.Level);

                DuckToFindHandler.SpawnDuck();
            }
        }
    }
}
