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
    public bool isGameActive;
    public bool isPlayerInvincible = false;
    private Coroutine invincibilityCoroutine;
    public TextMeshProUGUI invincibilityTimerText;



    public GameObject[] enemyPrefabs;
    private float spawnRangeX = 16;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 2.5f;
    private Coroutine spawnCoroutine;

    void Start()
    {
        score = 0;
        UpdateScore();
        UpdateLives();
        //AddScore(0);
        //AddLives(3);
        isGameActive = true;
        gameOverText.gameObject.SetActive(false);
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    public void AddLives(int value)
    {
        lives += value;
        UpdateLives();
        if (lives <= 0)
        {
            GameOver();
        }

    }

    private void UpdateLives()
    {
        Debug.Log("Lives = " + lives);
        // Update UI for lives here if you have one
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        Debug.Log("Score = " + score);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        Debug.Log("Game Over");
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startDelay);

        while (isGameActive)
        {
            SpawnRandomEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 1.1f, spawnPosZ);
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
    }

    public void SetInvincibility(bool status)
    {
        isPlayerInvincible = status;
    }

public void ActivateInvincibility(float duration)
    {
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
        }
        invincibilityCoroutine = StartCoroutine(InvincibilityTimer(duration));
    }

    private IEnumerator InvincibilityTimer(float duration)
    {
        isPlayerInvincible = true;
        invincibilityTimerText.gameObject.SetActive(true); // Show the timer UI
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            invincibilityTimerText.text = "Invincible: " + remainingTime.ToString("F1") + "s";
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }

        isPlayerInvincible = false;
        invincibilityTimerText.gameObject.SetActive(false); // Hide the timer UI
        invincibilityCoroutine = null;
    }
}