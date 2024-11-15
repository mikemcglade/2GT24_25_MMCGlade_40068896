using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
private int score;
private int lives = 3;
public TextMeshProUGUI scoreText;
public TextMeshProUGUI gameOverText;
//public bool gameOver = false;
public bool isGameActive;

    public GameObject[] enemyPrefabs; 
    private float spawnRangeX = 16;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        AddScore(0);
        AddLives(3);
        isGameActive = true;
       // spawnRandomEnemy();

        InvokeRepeating("spawnRandomEnemy", startDelay, spawnInterval);

       
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void AddLives(int value)
{
lives += value;

if (lives <= 0)
    {
    Debug.Log("Game Over");
    //gameOver = true;
    GameOver();
    lives = 0;
    }

Debug.Log("Lives = " + lives);
}

public void AddScore(int value)
{
    score += value;
    Debug.Log("Score = " + score);
    scoreText.text = "Score: " + score;
}

public void GameOver()
{
    gameOverText.gameObject.SetActive(true);
    isGameActive = false;
}

    void spawnRandomEnemy()
    {
        if(isGameActive)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 1.1f, spawnPosZ);
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
        }


    }
}
