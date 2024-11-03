using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    private float spawnRangeX = 16;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 2.5f;
    //private GameManager gameManagerScript;
    //private bool isGameActive = true;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnRandomEnemy", startDelay, spawnInterval);
        //gameManagerScript = GameObject.Find("Player").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void spawnRandomEnemy()
    {
        //while(isGameActive)
        //if (gameManagerScript.gameOver == false)
       // {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.5f, spawnPosZ);
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);

       // }
    }
}
