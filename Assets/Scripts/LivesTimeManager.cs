using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesTimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI timerText;
    
    private int lives;
    private float time;
    private bool resetLives;
    private bool resetTime;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        GameObject[] otherTransitionManagers = GameObject.FindGameObjectsWithTag("LivesTimeManager");
        if (otherTransitionManagers.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (!resetLives)
            {
                resetLives = true;
                lives = 0;
                livesText.text = $"{lives} Grooves Lost";
            }

            if (!resetTime)
            {
                resetTime = true;
                time = 0;
            }
        }
        else
        {
            resetLives = false;
            resetTime = false;
        }

        if (SceneManager.GetActiveScene().name != "Title" &&
            SceneManager.GetActiveScene().name != "Intro Cutscene")
        {
            time += Time.deltaTime;
            DisplayTime(time);
        }
        else if (SceneManager.GetActiveScene().name == "Title")
        {
            DisplayTime(time);
        }
        else if (SceneManager.GetActiveScene().name == "Intro Cutscene")
        {
            time = 0;
            DisplayTime(time);
        }
    }

    public void LoseLife()
    {
        lives++;
        livesText.text = $"{lives} Grooves Lost";
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
