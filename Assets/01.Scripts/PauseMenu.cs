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
        // ���� �޴�
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
        //Playerprefs�� ����
        PlayerPrefs.Save();
        menuSet.SetActive(false); // ���� �� ���� �簳
    }
    public void GameLoad()
    {

    }
    public void GameExit()
    {
        //SceneManager.LoadScene("");
        Debug.Log("����");
    }
    void PauseGame()
    {
        menuSet.SetActive(true);
        Time.timeScale = 0f; // ���� �ð� ����
        isPaused = true;
    }

    void ResumeGame()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1f; // ���� �ð� �ٽ� ����
        isPaused = false;
    }
}
