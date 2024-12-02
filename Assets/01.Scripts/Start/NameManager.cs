using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    public InputField textbox;
    public Button confirmBtn;

    private string playerName = null;

    private DataController datacontroller;

    private void Start()
    {
        confirmBtn.interactable = false;

        confirmBtn.onClick.AddListener(OnConfirm);

        datacontroller = FindObjectOfType<DataController>();
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
    }

}
