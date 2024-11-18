using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public Image lifeLostOverlay;
    public float fadeDuration = 0.5f;
    private int maxLives = 3;

    public GameObject[] enemyPrefabs;
    private float spawnRangeX = 16;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 2.5f;
    private Coroutine spawnCoroutine;
    public GameObject restartButton; // Assign this in the Inspector
    public GameObject gameOverScreen; // Assign this in the Inspector
    private bool isPaused = false;
    public RainController rainController;


    private int totalInteractableObjects = 3;
    private int interactedObjects = 0;
    public GameObject levelCompleteScreen;
    private InteractableObject lastInteractedObject;
    private bool isWaitingForLastInteraction = false;
    // adds UI visual effect for powerup
    private PlayerVisualEffect playerVisualEffect;

    void Start()
    {
        score = 0;
        lives = maxLives;
        UpdateScore();
        UpdateLives();
        isGameActive = true;
        //restartButton.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        gameOverScreen.SetActive(false);

        spawnCoroutine = StartCoroutine(SpawnEnemies());
        levelCompleteScreen.SetActive(false);

       // adds UI visual effect for powerup duration
        playerVisualEffect = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVisualEffect>();
        rainController = GameObject.Find("RainEffect").GetComponent<RainController>();
        SetRainIntensity(0.5f); // Start with medium rain
    }


    public void SetRainIntensity(float intensity)
    {
        rainController.SetRainIntensity(intensity);
    }
    public void AddLives(int value)
    {
        lives = Mathf.Clamp(lives + value, 0, maxLives);
        UpdateLives();
        if (lives <= 0)
        {
            GameOver();
        }
        else if (value < 0)
        {
            StartCoroutine(FlashLifeLostOverlay());
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
        restartButton.SetActive(true); // Show the restart button
        gameOverScreen.SetActive(true);

        isGameActive = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        Debug.Log("Game Over");
        // Waits 3 seconds before calling ReturnToStartScreen
        Invoke("ReturnToStartScreen", 3f); 

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
        // adds UI colour change for player
        playerVisualEffect.StartInvincibilityEffect(duration);
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

private IEnumerator FlashLifeLostOverlay()
    {
        // Fade in
        yield return StartCoroutine(FadeOverlay(0f, 1f));
        
        // Short pause at full opacity
        yield return new WaitForSeconds(0.1f);
        
        // Fade out
        yield return StartCoroutine(FadeOverlay(1f, 0f));
    }

    private IEnumerator FadeOverlay(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color overlayColor = lifeLostOverlay.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            lifeLostOverlay.color = new Color(overlayColor.r, overlayColor.g, overlayColor.b, alpha);
            yield return null;
        }

        lifeLostOverlay.color = new Color(overlayColor.r, overlayColor.g, overlayColor.b, endAlpha);
    }
public void RestartGame()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

private void ReturnToStartScreen()
    {
        SceneManager.LoadScene("StartScreen"); 
    }

public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ObjectInteracted(InteractableObject interactedObject)
    {
        interactedObjects++;
        lastInteractedObject = interactedObject;

        if (interactedObjects >= totalInteractableObjects)
        {
            isWaitingForLastInteraction = true;
        }
    }

public void InteractionComplete()
    {
        if (isWaitingForLastInteraction)
        {
            LevelComplete();
        }
    }
    private void LevelComplete()
    {
        isGameActive = false;
        levelCompleteScreen.SetActive(true);
        // Canan add more level complete logic here, such as stopping enemy spawns
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        Debug.Log("Level Complete!");
    }
}