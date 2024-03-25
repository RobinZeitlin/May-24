using ECS;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InteractWithDuck : MonoBehaviour
{
    public DuckToFindHandler DuckToFindHandler;

    public AudioClip quackSound;

    public UnityEvent OnDuckFound;

    private bool duckFound = false;

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
            if(hit.collider.gameObject.CompareTag("Duck") && !duckFound)
            {
                PlaySounds();
                ChangeLevel();
                duckFound = true;
            }
        }
    }

    public async void ChangeLevel()
    {
        await Task.Delay((int)(quackSound.length * 1200));

        GameManager.instance.Level++;

        OnDuckFound?.Invoke();

        LevelSystem levelSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<LevelSystem>();
        levelSystem.ChangeLevel(GameManager.instance.Level);

        duckFound = false;
        DuckToFindHandler.SpawnDuck();
    }

    public void PlaySounds()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = quackSound;

        audioSource.PlayOneShot(quackSound);

        audioSource.pitch = Random.Range(0.8f, 1.2f);

        Destroy(audioSource, quackSound.length);
    }
}
