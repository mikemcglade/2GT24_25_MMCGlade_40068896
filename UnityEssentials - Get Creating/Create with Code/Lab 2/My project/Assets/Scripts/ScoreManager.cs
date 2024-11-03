using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Text scoreText;
    int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        AddPoint();
    }

   void AddPoint()
   {
        currentScore++;
        scoreText.text = currentScore.ToString();
   }
}
