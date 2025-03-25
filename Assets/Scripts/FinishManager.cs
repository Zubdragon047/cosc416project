using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private Button BackToMainButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        BackToMainButton.onClick.AddListener(BackToMain);
    }

    private void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
