using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public Button StartBtn;
    public Button ContinueBtn;
    public Button ExitBtn;

    public CanvasGroup fade;

    private DataController datacontroller;
    private FadeController fadecontroller;

    private void Start()
    {
        datacontroller = FindObjectOfType<DataController>();
        fadecontroller = FindObjectOfType<FadeController>();

        StartBtn.onClick.AddListener(GameStart);
        ContinueBtn.onClick.AddListener(GameContinue);
        ExitBtn.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        //if jsonFile����
        if (datacontroller.isSave())
        {
            ContinueBtn.interactable = true;
        }
        else
        {
            ContinueBtn.interactable = false;
        }
        
    }

    private void GameStart()
    {
        datacontroller.newGameData();
        StartCoroutine(StartGame());
    }

    private void GameContinue()
    {
        datacontroller.LoadGameData();
        StartCoroutine(ContinueGame());
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    private IEnumerator StartGame()
    {
        //Fade
        yield return StartCoroutine(fadecontroller.FadeIn(fade));
        yield return new WaitForSeconds(1f);
        //�̸��Է�â���� �̵�
        SceneManager.LoadScene("00.Scenes/Start/NameScene");
        Debug.Log("���� ���۵�");
    }

    private IEnumerator ContinueGame()
    {
        //Fade
        yield return StartCoroutine(fadecontroller.FadeIn(fade));
        yield return new WaitForSeconds(1f);
        //������ �ʿ��� ����
    }

}
