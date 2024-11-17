using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{

    public AudioSource backgroundMusic;
    //public Animation startScreenAnimation;
    //public Button startButton;

    void Start()
    {
        // Play background music
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        // Play start screen animation
        // if (startScreenAnimation != null)
        // {
        //     startScreenAnimation.Play();
        // }

        // Add listener to start button
        //if (startButton != null)
        //{
        //    startButton.onClick.AddListener(StartGame);
        //}
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}