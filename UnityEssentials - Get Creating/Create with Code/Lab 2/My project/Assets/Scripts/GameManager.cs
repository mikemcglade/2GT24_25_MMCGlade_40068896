using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
private int score = 0;
private int lives = 3;
public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
}

}
