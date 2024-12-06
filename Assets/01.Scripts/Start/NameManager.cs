using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameManager : MonoBehaviour
{
    public InputField textbox;
    public Button confirmBtn;
    public CanvasGroup fade;

    private string playerName = null;

    private DataController datacontroller;
    FadeController fadecontroller;

    private void Start()
    {
        confirmBtn.interactable = false;

        confirmBtn.onClick.AddListener(OnConfirm);

        datacontroller = FindObjectOfType<DataController>();
        fadecontroller = FindObjectOfType<FadeController>();

        StartCoroutine(fadecontroller.FadeOut(fade));
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(textbox.text))
        {
            confirmBtn.interactable = true;
        }
    }

    private void OnConfirm()
    {
        datacontroller.newGameData();
        playerName = textbox.text;
        Debug.Log(playerName);

        datacontroller.gameData.name = playerName;
        //datacontroller.gameData.PuzzleProgress05[2] = true;
        datacontroller.SaveGameData();

        //스테이지 1로 이동
        SceneManager.LoadScene("00.Scenes/Prologue/Prologue 1");
    }

}
