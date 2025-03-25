using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private Button BackToMainButton;
    [SerializeField] private GameObject endLevelMenu;

    private void Awake()
    {
        BackToMainButton.onClick.AddListener(BackToMain);
        DisableEndLevelMenu();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EnableEndLevelMenu();
        }
    }

    private void EnableEndLevelMenu()
    {
        Time.timeScale = 0f; // save time somewher and display it
        endLevelMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
}
