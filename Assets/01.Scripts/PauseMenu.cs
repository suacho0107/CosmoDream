using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuSet;
    private bool isPaused = false;

    private FadeController fadeController;

    void Start()
    {
    }

    void Update()
    {
        // 서브 메뉴
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
    /*
    public void GameSave()
    {
        //Playerprefs로 저장
        PlayerPrefs.Save();
        menuSet.SetActive(false); // 저장 후 게임 재개
    }
    public void GameLoad()
    {

    }
    */
    public void GameExit()
    {
        //SceneManager.LoadScene("");
        DataController datacontroller;
        datacontroller = FindObjectOfType<DataController>();
        datacontroller.SaveGameData();

        Debug.Log("종료");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    void PauseGame()
    {
        menuSet.SetActive(true);
        Time.timeScale = 0f; // 게임 시간 정지
        AudioListener.pause = true;
        isPaused = true;
    }

    public void ResumeGame()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1f; // 게임 시간 다시 진행
        AudioListener.pause = false;
        isPaused = false;
    }
}
