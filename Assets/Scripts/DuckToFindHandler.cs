using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DuckToFindHandler : MonoBehaviour
{
    [Header("Duck References")]
    public GameObject duckPrefab;

    public List<GameObject> specialDucks = new List<GameObject>();
    public List<SteamAchievement> specialDuckACH = new List<SteamAchievement>();
    public List<GameObject> ducks;

    public DuckInfoSO duckInfo;

    private GameObject currentDuck;

    [Space(15)]

    [Header("Duck To Find")]
    public DuckToFind duckToFind;
    [Header("Ui References")]
    public Image hatShowCase;

    [HideInInspector] public float SpawnRange = 15;

    public SteamAchievement Crown;
    public SteamAchievement CowboyHat;
    public SteamAchievement MinerHat;

    public void Start()
    {
        SpawnDuck();
    }

    public void SpawnDuck()
    {
        if(duckToFind != DuckToFind.None)
            UnlockAchievement();

        SpawnSpecialDucks();

        if (currentDuck != null)
        {
            Destroy(currentDuck);
        }

        float randX = Random.Range(-SpawnRange * (GameManager.instance.Level * 0.2f), SpawnRange * (GameManager.instance.Level * 0.2f));
        float randY = Random.Range(-SpawnRange * (GameManager.instance.Level * 0.2f), SpawnRange * (GameManager.instance.Level * 0.2f));

        GameObject duck = Instantiate(duckPrefab, new Vector3(0 + randX, 0, 0 + randY), Quaternion.identity);
        currentDuck = duck;

        SetHatSprite();
        AssignHat(duck);
    }

    void UnlockAchievement()
    {
        switch (duckToFind)
        {
            case DuckToFind.CowboyHat:
                    CowboyHat.UnlockAchievement();
                break;

            case DuckToFind.Crown:
                    Crown.UnlockAchievement();
                break;

            case DuckToFind.MinerHat:
                    MinerHat.UnlockAchievement();
                break;
        }
    }
    void SpawnSpecialDucks()
    {
        ClearSpecialDucks();

        for(int i = 0; i < (int)(GameManager.instance.Level / 5); i++)
        {
            float randX = Random.Range(-SpawnRange * (GameManager.instance.Level * 0.2f), SpawnRange * (GameManager.instance.Level * 0.2f));
            float randY = Random.Range(-SpawnRange * (GameManager.instance.Level * 0.2f), SpawnRange * (GameManager.instance.Level * 0.2f));

            int chosenDuck = Random.Range(0, specialDucks.Count);
            GameObject thisDuck = Instantiate(specialDucks[chosenDuck], new Vector3(0 + randX, 0, 0 + randY), Quaternion.identity);
            specialDuckACH[chosenDuck].UnlockAchievement();

            ducks.Add(thisDuck);
        }
    }

    void ClearSpecialDucks()
    {
        if (ducks.Count > 0)
        {
            foreach (var duck in ducks)
            {
                Destroy(duck);
            }

            ducks.Clear();
        }
    }

    void SetHatSprite()
    {
        Debug.Log(duckInfo.hats.Count);
        duckToFind = (DuckToFind)Random.Range(1, duckInfo.hats.Count + 1);
        hatShowCase.sprite = duckInfo.hats[(int)duckToFind - 1].hatSprite;
    }
    public void AssignHat(GameObject _duck)
    {
        if (duckInfo.hats.Count > 0)
        {
            GameObject hat = duckInfo.hats[(int)duckToFind - 1].hatPrefab;

            GameObject hatEntity = Instantiate(hat, Vector3.zero, Quaternion.identity, _duck.transform);

            hatEntity.transform.localPosition = new Vector3(0, 1.165f, 0.15f);
            hatEntity.transform.localRotation = Quaternion.Euler(-105, 0, 0);
            hatEntity.transform.localScale = new Vector3(10f, 10f, 10f);
        }
    }
}