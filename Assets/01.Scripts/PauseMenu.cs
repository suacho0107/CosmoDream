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
        // ���� �޴�
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
        //Playerprefs�� ����
        PlayerPrefs.Save();
        menuSet.SetActive(false); // ���� �� ���� �簳
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

        Debug.Log("����");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    void PauseGame()
    {
        menuSet.SetActive(true);
        Time.timeScale = 0f; // ���� �ð� ����
        AudioListener.pause = true;
        isPaused = true;
    }

    public void ResumeGame()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1f; // ���� �ð� �ٽ� ����
        AudioListener.pause = false;
        isPaused = false;
    }
}
