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
    public Image invincibilityTimerImage;
    public Image[] livesImages;
    private int maxLives = 3;

    public GameObject[] enemyPrefabs;
    private float spawnRangeX = 16;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 2.5f;
    private Coroutine spawnCoroutine;

    void Start()
    {
        score = 0;
        lives = maxLives;
        UpdateScore();
        UpdateLives();
        isGameActive = true;
        gameOverText.gameObject.SetActive(false);
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    public void AddLives(int value)
    {
        lives = Mathf.Clamp(lives + value, 0, maxLives);
        UpdateLives();
        if (lives <= 0)
        {
            GameOver();
        }

    }

    private void UpdateLives()
    {
        Debug.Log("Lives = " + lives);
        for (int i = 0; i < livesImages.Length; i++)
        {
            livesImages[i].gameObject.SetActive(i < lives);
        }
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
        invincibilityTimerImage.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            invincibilityTimerImage.fillAmount = 1 - (elapsedTime / duration);
            yield return null;
        }

        isPlayerInvincible = false;
        invincibilityTimerImage.gameObject.SetActive(false);
        invincibilityCoroutine = null;
    }
}