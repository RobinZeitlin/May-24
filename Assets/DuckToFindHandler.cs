using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine;

public class DuckToFindHandler : MonoBehaviour
{
    public GameObject duckPrefab;

    public DuckInfoSO duckInfo;

    public float2 duckPos;

    public void Start()
    {
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
            int randomHatIndex = Random.Range(0, duckInfo.hats.Count);
            GameObject hat = duckInfo.hats[randomHatIndex];

            GameObject hatEntity = Instantiate(hat, Vector3.zero, Quaternion.identity, _duck.transform);

            hatEntity.transform.localPosition = new Vector3(0, 1.11f, 0.15f);
            hatEntity.transform.localRotation = Quaternion.Euler(-35, 0, 0);
            hatEntity.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
