using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    static float timer;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        pointsText.text = "YOUR TIME: " + Timer.niceTime;
    }

    public void RestartButton()
    {
        Timer.timer = 0;
        ScoreCounter.scoreValue = 0;
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {
        Timer.timer = 0;
        ScoreCounter.scoreValue = 0;
        SceneManager.LoadScene("Menu");
    }
}
