using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private bool isPaused = false;

    void Start()
    {
        // ������ �� �Ͻ����� �޴��� ��Ȱ��ȭ
        pauseMenuPanel.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // ���� �ð� �ٽ� ����
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // ���� �ð� ����
        isPaused = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // ���� �ð� �ٽ� ���� (�ʿ��)
        SceneManager.LoadScene("MainMenu"); // MainMenu��� ������ ����
    }
}
