using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    // Dictionary<int, Sprite> portraitData;

    // public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        // portraitData = new Dictionary<int, Sprite>();
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
        talkData.Add(100, new string[] { "현실 날짜와 시간이 똑같다.", "실시간 동기화가 되어있나.." });
        talkData.Add(200, new string[] { "꿈꾸는 인생을 이뤄봐요!" });

        //portraitData.Add(1000 + 0,);
        //portraitData.Add(1000 + 1,);
        //portraitData.Add(1000 + 2,);
        //portraitData.Add(1000 + 3,);

        // 스테이지 2
        talkData.Add(211, new string[] { "화장대 위에 사진 퍼즐이 놓여있다." });
        talkData.Add(231, new string[] { "베개에 사진이 끼어있다." });
        talkData.Add(241, new string[] { "가족앨범이다. 분명 우리 가족사진과 똑같지만….. 내가 빠져있다.",
        "맨 끝에 이건 뭐지?",
        "...",
        "가족앨범 사이에 끼워져 있는 가위를 얻었다." });
        talkData.Add(242, new string[] { "퍼즐을 찾았다." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
