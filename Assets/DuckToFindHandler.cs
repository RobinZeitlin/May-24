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

    [Space(15)]

    [Header("Duck To Find")]
    public DuckToFind duckToFind;
    [Header("Ui References")]
    public Image hatShowCase;

    [HideInInspector] public float2 duckPos = new float2(30,30);

    public void Start()
    {
        if(duckToFind == DuckToFind.None)
        {
            duckToFind = (DuckToFind)Random.Range(1, 7);
        }

        hatShowCase.sprite = duckInfo.hats[(int)duckToFind - 1].hatSprite;

        SpawnDuck();
    }

    public void SpawnDuck()
    {
        float randX = Random.Range(-duckPos.x, duckPos.x);
        float randY = Random.Range(-duckPos.y, duckPos.y);

        GameObject duck = Instantiate(duckPrefab, new Vector3(randX, 0, randY), Quaternion.identity);
        AssignHat(duck);
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