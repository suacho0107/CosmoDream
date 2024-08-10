using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        // npc
        talkData.Add(1000, new string[] { "그것도 모르고 게임을 샀어요? 제2의 인생을 사는 게임이잖아요." });
        talkData.Add(2000, new string[] { "코스모역에 오신 것을 환영합니다! 즐거운 메타버스 생활 되세요!" });
        talkData.Add(3000, new string[] { "헉 지정 영역 밖으로 빠져나오다니! 여긴 웬일이야?",
            "어? 아니구나. 죄송합니다, 친한 NPC랑 착각했어요!" });

        // obj
        talkData.Add(100, new string[] { "현실 시간과 날짜랑 똑같다.", "실시간 동기화가 되어있나보다." });
        talkData.Add(200, new string[] { "꿈꾸는 인생을 이뤄봐요!" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
