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
        talkData.Add(11010, new string[] { "여긴… 게임 세계? 근처에 뭐가 있나 보자." });
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

        // 210~240
        talkData.Add(21001, new string[] { "화장대 위에 사진 퍼즐이 놓여있다." });
        talkData.Add(23001, new string[] { "베개에 사진이 끼어있다." });
        talkData.Add(24001, new string[] { "가족앨범이다. 분명 우리 가족사진과 똑같지만….. 내가 빠져있다.",
        "맨 끝에 이건 뭐지?",
        "...",
        "가족앨범 사이에 끼워져 있는 가위를 얻었다." });
        talkData.Add(24002, new string[] { "퍼즐을 찾았다." });


        talkData.Add(40001, new string[] { "그 당시에 유행했던 LP판. \nLP판 3개를 찾아서 리듬게임을 해보자." }); //LP판
        talkData.Add(40002, new string[] { "13년 전에 개봉한 영화다. \n포스터 뒤에 LP판이 아슬아슬하게 숨겨져 있다.", "LP판을 얻었다." }); //포스터
        talkData.Add(40003, new string[] { "서랍 속에 여러가지 잡동사니들이 들어있다. \n필기구들이 어지럽혀져 있다.", "필기구들 사이에 파묻혀 있는 LP판을 찾아냈다!" }); //서랍
        talkData.Add(40004, new string[] { "책들 사이에 lp판이 끼어있다. \n손상되지 않았어야 할텐데…." }); //책장(lp판 획득)
        talkData.Add(40005, new string[] { "책상 위에 메모지와 읽다 만 책, \n그리고… 송곳이 보인다. ",
                "책을 읽다 메모를 한 듯 하다.", "대충 아날로그로 되어있는 인간의 기억을 디지털 신호로 바꾼다…는 내용이다. \n내 동생이 좋아할 만한 책이다." , "근데 왜 책상 위에 송곳이 있는 것일까? \n위험하니 치워야지.", 
                "송곳을 챙겼다." }); //책상
        talkData.Add(40006, new string[] { "그 당시에 금지됐던 논란의 금서다.", "조금 불쾌하다. \n어떻게 사람을 데이터화 시키지?", "만년필을 발견했다." }); //초록색 책
        talkData.Add(40007, new string[] { "응…?" }); //노란책 책장
        talkData.Add(40008, new string[] { "마지막 음악까지 연주했다." }); //클리어

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
