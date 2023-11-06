using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text healthText;
    public static GameController instance;
    public GameObject pauseObj;
    public GameObject gameOverObj;
    private bool isPaused;
    public int score;
    public Text scoreText;
    public int totalScore;

    void Awake()
    {
        instance = this;
        isPaused = false;
        Time.timeScale = 1;
        totalScore = PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame(); 
    }

     /*public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();

        PlayerPrefs.SetInt("Score", score + totalScore);
    }*/

    public void UpdateLives(int value)
    {
        healthText.text = "x " + value.ToString();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);

            print(isPaused);

            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

        }

    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        gameOverObj.SetActive(true);
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
