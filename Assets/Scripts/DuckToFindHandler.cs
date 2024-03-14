using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.UI;

public class DuckToFindHandler : MonoBehaviour
{
    [Header("Duck References")]
    public GameObject duckPrefab;

    public DuckInfoSO duckInfo;

    private GameObject currentDuck;

    [Space(15)]

    [Header("Duck To Find")]
    public DuckToFind duckToFind;
    [Header("Ui References")]
    public Image hatShowCase;

    [HideInInspector] public float SpawnRange = 12;

    public void Start()
    {
        SpawnDuck();
    }

    public void SpawnDuck()
    {
        if(currentDuck != null)
        {
            Destroy(currentDuck);
        }

        float randX = Random.Range(-SpawnRange, SpawnRange);
        float randY = Random.Range(-SpawnRange, SpawnRange);

        GameObject duck = Instantiate(duckPrefab, new Vector3(0 + randX, 0, 0 + randY), Quaternion.identity);
        currentDuck = duck;

        SetHatSprite();
        AssignHat(duck);
    }

    void SetHatSprite()
    {
        duckToFind = (DuckToFind)Random.Range(1, 7);
        hatShowCase.sprite = duckInfo.hats[(int)duckToFind - 1].hatSprite;
    }
    public void AssignHat(GameObject _duck)
    {
        if (duckInfo.hats.Count > 0)
        {
            GameObject hat = duckInfo.hats[(int)duckToFind - 1].hatPrefab;

            GameObject hatEntity = Instantiate(hat, Vector3.zero, Quaternion.identity, _duck.transform);

            hatEntity.transform.localPosition = new Vector3(0, 1.11f, 0.15f);
            hatEntity.transform.localRotation = Quaternion.Euler(-35, 0, 0);
            hatEntity.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}