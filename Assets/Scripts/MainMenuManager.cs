using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private Button EasyButton;
    [SerializeField] private Button HardButton;
    [SerializeField] private Button ControlsButton;
    [SerializeField] private Button ControlsBackButton;
    [SerializeField] private Button TutorialButton;

    // high scores
    [SerializeField] private ScoreManager scoreManager;
    private int levelOne = 1;
    private int levelTwo = 2;
    [SerializeField] private TextMeshProUGUI HighScoresLevel1;
    [SerializeField] private TextMeshProUGUI HighScoresLevel2;

    private void Awake()
    {
        MainMenuPanel.SetActive(true);
        ControlsPanel.SetActive(false);
        EasyButton.onClick.AddListener(StartEasyLevel);
        HardButton.onClick.AddListener(StartHardLevel);
        ControlsButton.onClick.AddListener(OpenControlsMenu);
        ControlsBackButton.onClick.AddListener(CloseControlsMenu);
        TutorialButton.onClick.AddListener(StartTutorial);

        // getting the high scores
        List<float> scoresOne = scoreManager.LoadScores(levelOne);
        for (int i = 0; i < scoresOne.Count; i++)
        {
            HighScoresLevel1.text += $"{i + 1}.  {ScoreManager.FormatTime(scoresOne[i])}\n";
        }
        List<float> scoresTwo = scoreManager.LoadScores(levelTwo);
        for (int i = 0; i < scoresTwo.Count; i++)
        {
            HighScoresLevel2.text += $"{i + 1}.  {ScoreManager.FormatTime(scoresTwo[i])}\n";
        }

    }

    private void StartEasyLevel()
    {
        MainMenuPanel.SetActive(false);
        SceneManager.LoadScene("LevelA_FullUI");
    }

    private void StartHardLevel()
    {
        
    }

    private void StartTutorial()
    {
        MainMenuPanel.SetActive(false);
        SceneManager.LoadScene("TutorialLevel");
    }

    private void OpenControlsMenu()
    {
        MainMenuPanel.SetActive(false);
        ControlsPanel.SetActive(true);
    }

    private void CloseControlsMenu()
    {
        ControlsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);

    }
}
