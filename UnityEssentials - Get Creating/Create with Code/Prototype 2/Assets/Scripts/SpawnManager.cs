using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabAnimals;
    private float spawnRangeX = 20;
    private float spawnPosZ = 20;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", 2, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
     
     
    }

    void SpawnRandomAnimal()
    {
         int animalIndex = Random.Range(0, prefabAnimals.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

            Instantiate(prefabAnimals[animalIndex], spawnPos, prefabAnimals[animalIndex].transform.rotation);

    }
}
