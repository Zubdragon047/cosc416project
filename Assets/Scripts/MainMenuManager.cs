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
    private void Awake()
    {
        MainMenuPanel.SetActive(true);
        ControlsPanel.SetActive(false);
        EasyButton.onClick.AddListener(StartEasyLevel);
        HardButton.onClick.AddListener(StartHardLevel);
        ControlsButton.onClick.AddListener(OpenControlsMenu);
        ControlsBackButton.onClick.AddListener(CloseControlsMenu);
    }

    private void StartEasyLevel()
    {
        MainMenuPanel.SetActive(false);
        SceneManager.LoadScene("LevelA_FullUI");
    }

    private void StartHardLevel()
    {
        
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
