using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFences : MonoBehaviour
{
    public static PlaceFences instance;

    public GameObject fencePrefab; // Assign your fence prefab in the Inspector

    private int numberOfFences = 10; // Total number of fences to place

    private float radius = 5f; // Radius of the circle
    private float raycastDistance = 10f; // Distance to raycast downwards to find the surface

    private void Awake()
    {
        instance = this;

        ResetFences();
    }

    public void ResetFences()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        PlaceFencesInCircle();
    }

    void PlaceFencesInCircle()
    {
        radius = 12 + (15 * (GameManager.instance.Level * 0.12f));
        numberOfFences = (int)(radius * 4 - (radius / 3));

        for (int i = 0; i < numberOfFences; i++)
        {
            // Calculate angle and position for each fence
            float angle = i * Mathf.PI * 2 / numberOfFences;
            Vector3 position = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius;
            position += transform.position; // Adjust position based on this object's position

            RaycastHit hit;
            if (Physics.Raycast(position + Vector3.up * 5, Vector3.down, out hit, raycastDistance))
            {
                GameObject fence = Instantiate(fencePrefab, hit.point, Quaternion.identity, transform);

                Vector3 directionToCenter = (fence.transform.position - Vector3.zero).normalized;
                Quaternion rotationToCenter = Quaternion.LookRotation(directionToCenter);

                Quaternion rotation = Quaternion.LookRotation(directionToCenter, hit.normal);

                fence.transform.rotation = rotation;
            }
        }

    }
}
