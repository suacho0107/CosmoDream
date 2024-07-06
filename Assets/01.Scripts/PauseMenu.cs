using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private bool isPaused = false;

    void Start()
    {
        // 시작할 때 일시정지 메뉴를 비활성화
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
        Time.timeScale = 1f; // 게임 시간 다시 진행
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // 게임 시간 정지
        isPaused = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // 게임 시간 다시 진행 (필요시)
        SceneManager.LoadScene("MainMenu"); // MainMenu라는 씬으로 변경
    }
}
