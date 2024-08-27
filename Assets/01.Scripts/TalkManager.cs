using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // 110
        talkData.Add(11011, new string[] { "플레이어:여긴 뭐하는 곳이죠…?",
        "그것도 모르고 게임을 샀어요? 제2의 인생을 사는 게임이잖아요." });
        talkData.Add(11012, new string[] { "코스모역에 오신 것을 환영합니다!", "즐거운 메타버스 생활 되세요!" });
        talkData.Add(11013, new string[] { "헉 지정 영역 밖으로 빠져나오다니! 여긴 웬일이야?",
        "플레이어:...네?", "어? 아니구나. 죄송합니다, 친한 NPC랑 착각했어요!" });
        talkData.Add(11004, new string[] { "현실 날짜와 시간이 똑같다.", "실시간 동기화가 되어있나.." });
        talkData.Add(11005, new string[] { "꿈꾸는 인생을 이뤄봐요!" });

        // 120
        talkData.Add(12001, new string[] { "굉장히 친숙하게 생겼다. 우리 집 아파트와 거의 유사하다." });
        talkData.Add(12002, new string[] { "(...)" });
        talkData.Add(12003, new string[] { "오랜만에 접속하신 것 같은데..." });
        talkData.Add(12004, new string[] { "(...)" });

        // 131
        talkData.Add(13111, new string[] { "YUNOH:안녕! 새로운 친구네? 난 이곳의 관리자 유노야.",
        "플레이어:(익숙한 느낌이 든다.) 안녕. 이곳에 처음 와봤는데 뭐부터 하면 좋을까?",
        "YUNOH:내가 안내해줄게! 일단 이 아파트부터 구경해볼까?"});

        // 132
        talkData.Add(13211, new string[] { "YUNOH:여기는 드림 아파트야. 내가 지었어!",
        "플레이어:(아파트 이름 진짜 대충 지었다….) 경치가 참 좋네.",
        "YUNOH:그치? 이제 여기가 너의 거주 공간이 될 거야.", "YUNOH:... ...", "YUNOH:음...", "플레이어:왜 그래?",
        "YUNOH:빈 방이 없어… 미안해. \n새로운 공간을 다 지을 때까지 내 집에서 지내야겠다! 내 집 구경시켜줄게!"});

        // 141
        talkData.Add(14111, new string[] { "플레이어:(우리 집을 뒤집어 놓은 것 같이 생겼어.)"});
        talkData.Add(14102, new string[] { "그 방이 아니야!"});
        talkData.Add(14103, new string[] { "어릴 때 우리 집에 있었던 쇼파랑 똑같이 생겼다."});
        talkData.Add(14304, new string[] { "도시 전체가 보인다. 메타버스 세계라 그런가, 확실히 비현실적이야."});

        // 151
        talkData.Add(15111, new string[] { "YUNOH:여기가 내 방이야! 내가 직접 만든 퍼즐들이 있으니까 심심하진 않을거야.",
        "퍼즐들을 풀어볼래? (선택지)" });
        talkData.Add(15102, new string[] { "YUNOH:퍼즐은 풀고 가!"});

        // 152
        talkData.Add(15201, new string[] { "플레이어:부모님이랑 너야?", "YUNOH:응!", "플레이어:너 혹시 형제는 없어?",
        "YUNOH:음... 글쎄?" });
        talkData.Add(15202, new string[] { "YUNOH:그건 다른 NPC들이랑 찍은 스크린샷이야!" });

        // 153
        talkData.Add(15301, new string[] { "이건 선 하나로 모든 점들을 빈틈없이 이어주면 돼. 쉽지?" });


               
        //portraitData.Add(1000 + 0,);
        //portraitData.Add(1000 + 1,);
        //portraitData.Add(1000 + 2,);
        //portraitData.Add(1000 + 3,);

        // 210±240
        talkData.Add(21001, new string[] { "화장대 위에 사진 퍼즐이 놓여있다." });
        talkData.Add(23001, new string[] { "베개에 사진이 끼어있다." });
        talkData.Add(24001, new string[] { "가족앨범이다. 분명 우리 가족사진과 똑같지만….. 내가 빠져있다.",
        "맨 끝에 이건 뭐지?",
        "...",
        "가족앨범 사이에 끼워져 있는 가위를 얻었다." });
        talkData.Add(24002, new string[] { "퍼즐을 찾았다." });

    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id) || talkIndex >= talkData[id].Length)
        {
            return null;
        }

        var talkEntry = talkData[id][talkIndex];
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
        if (!talkData.ContainsKey(id) || talkIndex >= talkData[id].Length)
        {
            speakerName = "";
            return null;
        }

        var talkEntry = talkData[id][talkIndex];
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
