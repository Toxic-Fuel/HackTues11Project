using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bots : MonoBehaviour
{
    [SerializeField]
    private GameObject border;

    [SerializeField]
    private GameObject botPrefab;

    private void SpawnBots(int numberOfBots)
    {
        Vector3 borderPosition = border.transform.position;
        Vector3 borderScale = border.transform.localScale;
        Vector3 botScale = botPrefab.transform.localScale;

        for (int i = 0; i < numberOfBots; i++)
        {
            float randomX = Random.Range(
                borderPosition.x - borderScale.x / 2,
                borderPosition.x + borderScale.x / 2
            );
            float randomZ = Random.Range(
                borderPosition.z - borderScale.z / 2,
                borderPosition.z + borderScale.z / 2
            );

            Vector3 randomPosition = new Vector3(randomX, botScale.y / 2, randomZ);

            Instantiate(botPrefab, randomPosition, Quaternion.identity);
            Debug.Log(randomPosition);
        }
    }

    void Start()
    {
        SpawnBots(2);
    }
}
