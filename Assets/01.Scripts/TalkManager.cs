using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text UINameText;
    public Text UITalkText;
    // public Image portraitImg;
    public string playerName; // 나중에 다 구현되면 받아서 사용
    public int talkIndex;

    ObjData objData;
    GameManager gameManager;
    PlayerController playerController;
    DataController datacontroller;
    // public Sprite[] portraitArr;
    TalkData talkDataScript;
    Dictionary<int, Choice[]> choiceData = new Dictionary<int, Choice[]>();


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<PlayerController>();
        talkDataScript = FindObjectOfType<TalkData>();
        datacontroller = FindObjectOfType<DataController>();
        talkPanel.SetActive(false);

        playerName = datacontroller.gameData.name;
    }

    public void Talk(int id)
    {
        gameManager.isTalk = true;
        playerController.SetMove(false);
        string speakerName;
        string talkData = GetTalk(id, talkIndex, out speakerName);

        if (talkData == null)
        {
            EndTalk(id);
            return;
        }

        UINameText.text = (speakerName == "플레이어") ? playerName : speakerName;

        if (talkData.Contains("(선택지)"))
        {
            string talkData_trim = talkData.Replace("(선택지)", "").Trim();
            UITalkText.text = talkData_trim;
            talkPanel.SetActive(true);
            ChoiceManager choiceManager = FindObjectOfType<ChoiceManager>();
            choiceManager.ShowChoices();
        }
        else
        {
            UITalkText.text = talkData;
            talkPanel.SetActive(true); // 대화 패널 표시
            talkIndex++; // 다음 대사로 이동
        }

        // portraitImg.sprite = talkManager.GetPortrait(id,int.Parse(talkData.Split(':')[1]));
        // portraitImg.color = isNpc ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
    }

    public string GetTalk(int id, int talkIndex)
    {
        string[] talkArray = talkDataScript.GetTalkData(id);

        if (talkArray == null || talkIndex >= talkArray.Length)
        {
            return null;
        }

        var talkEntry = talkArray[talkIndex];
        var parts = talkEntry.Split(':');

        if (parts.Length == 2)
        {
            return parts[1]; // 대사만 반환
        }
        else
        {
            return talkEntry; // 이름 없는 대사 반환
        }
    }

    public string GetTalk(int id, int talkIndex, out string speakerName)
    {
        string[] talkArray = talkDataScript.GetTalkData(id);

        if (talkArray == null || talkIndex >= talkArray.Length)
        {
            speakerName = "";
            return null;
        }

        var talkEntry = talkArray[talkIndex];
        var parts = talkEntry.Split(':');

        if (parts.Length == 2)
        {
            speakerName = parts[0];
            return parts[1];
        }
        else
        {
            speakerName = "";
            return talkEntry;
        }
    }

    public void EndTalk(int talkId)
    {
        talkPanel.SetActive(false);
        gameManager.isTalk = false;
        talkIndex = 0;

        if (talkId == 13211) // 1-3-2
        {
            playerController.SetMove(false);
        }
        else
            playerController.SetMove(true);
        
        if (talkId == 24001) // 가족앨범 - 가위
        {
            datacontroller.gameData._scissors = true;
            Debug.Log("플레이어가 가위를 획득했습니다.");
        }
        else if (talkId == 31006) // 가족앨범 - 화이트
        {
            datacontroller.gameData._white = true;
            Debug.Log("플레이어가 화이트를 획득했습니다.");
        }
        else if(talkId==40005)
        {
            datacontroller.gameData._awl = true;
            Debug.Log("플레이어가 송곳을 획득했습니다.");
        }
        else if(talkId== 51001)
        {
            datacontroller.gameData._hammer = true;
            Debug.Log("플레이어가 망치를 획득했습니다.");
        }

    }
}