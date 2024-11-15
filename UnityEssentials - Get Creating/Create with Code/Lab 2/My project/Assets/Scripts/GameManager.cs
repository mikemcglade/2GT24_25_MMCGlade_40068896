using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
private int score;
public TextMeshProUGUI scoreText;
public TextMeshProUGUI gameOverText;

private int lives = 3;
public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        AddScore(0);
        AddLives(3);

        gameOverText.gameObject.SetActive(true);
       
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
    gameOver = true;
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

}
