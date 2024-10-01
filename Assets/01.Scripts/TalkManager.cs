using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{    
    public GameObject talkPanel;
    public Text UINameText;
    public Text UITalkText;
    // public Image portraitImg;
    public string playerName = "플레이어"; // 나중에 다 구현되면 받아서 사용
    public int talkIndex;

    ObjData objData;
    GameManager gameManager;
    PlayerController playerController;

    // public Sprite[] portraitArr;
    TalkData talkDataScript;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<PlayerController>();
        talkDataScript = FindObjectOfType<TalkData>();
        talkPanel.SetActive(false);
    }

    public void Talk(int id)
    {
        gameManager.isTalk = true;
        playerController.SetMove(false);
        string speakerName;
        string talkData = GetTalk(id, talkIndex, out speakerName);

        if (talkData == null)
        {
            EndTalk(id); // 대화 종료 후 이벤트변수 처리
            talkPanel.SetActive(false);
            gameManager.isTalk = false;
            playerController.SetMove(true);
            talkIndex = 0;
            return;
        }

        // UI 업데이트
        UINameText.text = (speakerName == "플레이어") ? playerName : speakerName;
        UITalkText.text = talkData;
        
        // portraitImg.sprite = talkManager.GetPortrait(id,int.Parse(talkData.Split(':')[1]));
        // portraitImg.color = isNpc ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
        
        talkPanel.SetActive(true);
        talkIndex++;
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
            return talkEntry; // 이름 없는 대사 반환ㅇ
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

    void EndTalk(int talkId)
    {
        if (talkId == 24001) // 가족앨범 - 가위
        {
            gameManager.hasScissors = true;
            Debug.Log("플레이어가 가위를 획득했습니다.");
        }
    }
}
