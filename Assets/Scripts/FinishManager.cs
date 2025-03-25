using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private Button BackToMainButton;
    [SerializeField] private Button ReplayLevelButton;
    [SerializeField] private GameObject endLevelMenu;

    // for the level time mechanics
    private bool timeStarted = false;
    private float startTime;
    [SerializeField] private TextMeshProUGUI currentTime;
    
    // end level times
    [SerializeField] private TextMeshProUGUI finalTime;
    [SerializeField] private TextMeshProUGUI topTimes;
    [SerializeField] private int currentLevel;
    [SerializeField] private ScoreManager scoreManager;

    private void Awake()
    {
        BackToMainButton.onClick.AddListener(BackToMain);
        ReplayLevelButton.onClick.AddListener(ReplayLevel);
        DisableEndLevelMenu();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            timeStarted = true;
            startTime = Time.time;
        }

        if (timeStarted) {
            var guiTime = Time.time - startTime;

            int minutes = (int)(guiTime / 60);
            int seconds = (int)(guiTime % 60);
            int fraction = (int)((guiTime * 100) % 100);

            String time = String.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
            currentTime.SetText(time);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            timeStarted = false;

            var guiTime = Time.time - startTime;

            int minutes = (int)(guiTime / 60);
            int seconds = (int)(guiTime % 60);
            int fraction = (int)((guiTime * 100) % 100);

            String time = String.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
            currentTime.SetText(time);

            EnableEndLevelMenu();
        }
    }

    private void EnableEndLevelMenu()
    {
        Time.timeScale = 0f; // save time somewhere and display it
        endLevelMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        finalTime.SetText(currentTime.text);

        // saving the score
        var guiTime = Time.time - startTime;
        scoreManager.SaveScore(currentLevel, guiTime);

        // Load and display top 3 times
        List<float> scores = scoreManager.LoadScores(currentLevel);
        topTimes.text = "Leaderboard:\n";
        for (int i = 0; i < scores.Count; i++)
        {
            topTimes.text += $"{i + 1}.  {ScoreManager.FormatTime(scores[i])}\n";
        }
    }

    public void DisableEndLevelMenu()
    {
        Time.timeScale = 1f;
        endLevelMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
