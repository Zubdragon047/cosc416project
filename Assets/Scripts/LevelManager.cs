using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject pauseMenu;
    private bool isPauseMenuActive;
    public bool IsPauseMenuActive => isPauseMenuActive;
    [SerializeField] private Button restartLevelButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private GameObject pressSpacePrompt;

    private void Awake()
    {
        // for pause menu
        inputManager.OnPauseMenu.AddListener(TogglePauseMenu);
        DisablePauseMenu();
        restartLevelButton.onClick.AddListener(RestartLevel);
        mainMenuButton.onClick.AddListener(GoToMainMenu);

        // for initial prompt
        inputManager.OnLevelStart.AddListener(DisablePressSpacePrompt);
        pressSpacePrompt.SetActive(true);

    }

    // initial prompt to press Space
    private void DisablePressSpacePrompt()
    {
        pressSpacePrompt.SetActive(false);
    }

    // Controlling Pause Menu in the levels
    private void TogglePauseMenu()
    {
        if(isPauseMenuActive) DisablePauseMenu();
        else EnablePauseMenu();
    }

    private void EnablePauseMenu()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPauseMenuActive = true;
    }

    public void DisablePauseMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPauseMenuActive = false;
    }

    // button functions
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
