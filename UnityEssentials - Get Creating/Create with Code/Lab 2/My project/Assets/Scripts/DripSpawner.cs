using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripSpawner : MonoBehaviour
{
    public GameObject dripPrefab;
    public float spawnInterval = 5f;
    public float spawnRangeX = 10f;
    public float spawnRangeZ = 10f;

    private void Start()
    {
        StartCoroutine(SpawnDripsRoutine());
    }

    private IEnumerator SpawnDripsRoutine()
    {
        while (true)
        {
            SpawnDrip();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnDrip()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, randomZ);
        Instantiate(dripPrefab, spawnPosition, Quaternion.identity);
    }
}
