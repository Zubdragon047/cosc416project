using UnityEngine;



public class PauseMenuManager : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject pauseMenu;
    private bool isPauseMenuActive;
    public bool IsPauseMenuActive => isPauseMenuActive;

    private void Awake()
    {
        // for the Pause Menu
        inputManager.OnPauseMenu.AddListener(TogglePauseMenu);
        DisablePauseMenu();
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

}
