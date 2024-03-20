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

    public GameObject duckOnScooter;
    public List<GameObject> ducks;

    public DuckInfoSO duckInfo;

    private GameObject currentDuck;

    [Space(15)]

    [Header("Duck To Find")]
    public DuckToFind duckToFind;
    [Header("Ui References")]
    public Image hatShowCase;

    [HideInInspector] public float SpawnRange = 15;

    public void Start()
    {
        SpawnDuck();
    }

    public void SpawnDuck()
    {
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

    void SpawnSpecialDucks()
    {
        ClearSpecialDucks();

        for(int i = 0; i < (int)(GameManager.instance.Level / 5); i++)
        {
            float randX = Random.Range(-SpawnRange * (GameManager.instance.Level * 0.2f), SpawnRange * (GameManager.instance.Level * 0.2f));
            float randY = Random.Range(-SpawnRange * (GameManager.instance.Level * 0.2f), SpawnRange * (GameManager.instance.Level * 0.2f));

            GameObject thisDuck = Instantiate(duckOnScooter, new Vector3(0 + randX, 0, 0 + randY), Quaternion.identity);

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