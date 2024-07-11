using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuSet;
    private bool isPaused = false;
    void Start()
    {
        GameLoad();
    }

    void Update()
    {
        // 서브 메뉴
        if (Input.GetButtonDown("Cancel"))
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

    public void GameSave()
    {
        //Playerprefs로 저장
        PlayerPrefs.Save();
        menuSet.SetActive(false); // 저장 후 게임 재개
    }
    public void GameLoad()
    {

    }
    public void GameExit()
    {
        //SceneManager.LoadScene("");
        Debug.Log("종료");
    }
    void PauseGame()
    {
        menuSet.SetActive(true);
        Time.timeScale = 0f; // 게임 시간 정지
        isPaused = true;
    }

    void ResumeGame()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1f; // 게임 시간 다시 진행
        isPaused = false;
    }
}
