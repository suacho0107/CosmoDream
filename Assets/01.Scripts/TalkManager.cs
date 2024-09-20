using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public Sprite[] portraitArr;
    TalkData talkDataScript;

    void Start()
    {
        talkDataScript = FindObjectOfType<TalkData>();
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
}
