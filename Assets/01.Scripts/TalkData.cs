using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkData : MonoBehaviour
{
    // Dictionary<int, Sprite> portraitData;
    Dictionary<int, string[]> talkData;
    Dictionary<int, Choice[]> choiceData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        choiceData = new Dictionary<int, Choice[]>();
        // portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(6, new string[] {"YUNOH:여기가 내 집이야."});
        //Prologue
        // 910
        talkData.Add(91000, new string[] { "난 남은 짐을 마저 정리해야 한다. ", "태어나서 첫 이사. 부모님은 먼저 새로운 집으로 가셨다." });
        talkData.Add(91001, new string[] { "내 모습이다." });
        talkData.Add(91002, new string[] { "잘 정리해둔 이삿짐.", "옷가지와 여러가지 물건들이 담겨져 있다." });
        talkData.Add(91003, new string[] { "도시 전체가 보인다. 이제 이 도시도 안녕이겠지." });
        talkData.Add(91004, new string[] { "이제 거실은 끝. 남은 건 동생방이겠지 ..." });

        //920
        talkData.Add(92001, new string[] { "아주 어릴 때 찍은 동생과 나." });
        talkData.Add(92002, new string[] { "가족사진이다.", "나와 부모님, 동생이 있다.","", "친척어른 1 : OO이도 참 안타깝지… 어린 애인데 아파서 떠나버리다니….", "친척어른 2 : 그러게 말이야…. 재능 있는 아이였는데…." +
            "“그 때는 윤오도 나도 어렸었지. “","동생이 떠나고 난 뒤로 이 방은 쓰지 않게 되었다." });
        talkData.Add(92003, new string[] { "정리할 건 이거밖에 없네." });


        // 110
        talkData.Add(11011, new string[] { "플레이어:여긴 뭐하는 곳이죠…?",
        "타 플레이어:그것도 모르고 게임을 샀어요? 제2의 인생을 사는 게임이잖아요." });
        talkData.Add(11012, new string[] { "코스모역에 오신 것을 환영합니다!", "즐거운 메타버스 생활 되세요!" });
        talkData.Add(11013, new string[] { "타 플레이어:헉?! 지정 영역 밖으로 빠져나오다니! \n여긴 웬일이야?",
        "플레이어:... 네?", "타 플레이어:어? 아니구나. 죄송합니다, 친한 NPC랑 착각했어요!" });
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

        // 141~143
        talkData.Add(14102, new string[] { "YUNOH:그 방이 아니야!"});
        talkData.Add(14103, new string[] { "어릴 때 우리 집에 있었던 쇼파랑 똑같이 생겼다."});
        talkData.Add(14304, new string[] { "도시 전체가 보인다. 메타버스 세계라 그런가, 확실히 비현실적이야."});
        talkData.Add(14401, new string[] { "내 모습이다." });

        // 151
        talkData.Add(15111, new string[] { "YUNOH:퍼즐들을 풀어볼래? (선택지)"});

        choiceData.Add(15111, new Choice[] {
            new Choice("응", 15),
            new Choice("아직", -1) });

        talkData.Add(15102, new string[] { "YUNOH:퍼즐은 풀고 가!"});
        talkData.Add(15103, new string[] { "YUNOH:이 집 곳곳에 퍼즐을 뒀어! 너의 집을 만들 동안 여기서 편히 지내길 바라."});

        // 152
        talkData.Add(15201, new string[] { "플레이어:부모님이랑 너야?", "YUNOH:응!", "플레이어:너 혹시 형제는 없어?",
        "YUNOH:음... 글쎄?" });
        talkData.Add(15202, new string[] { "YUNOH:그건 다른 NPC들이랑 찍은 스크린샷이야!" });

        // 153
        talkData.Add(15301, new string[] { "장난감들이 들어 있다." });
               
        //portraitData.Add(1000 + 0,);
        //portraitData.Add(1000 + 1,);
        //portraitData.Add(1000 + 2,);
        //portraitData.Add(1000 + 3,);

        // 210~240
        talkData.Add(21000, new string[] { "이건 이미 푼 퍼즐이다." });
        talkData.Add(21001, new string[] { "화장대 위에 사진 퍼즐이 놓여있다." });
        talkData.Add(22001, new string[] { "어릴 때 내가 부모님께 선물로 드린 오르골이 있다.",
        "내가 유치원에서 조립시간에 만든 것이다.", "내 물건들이 왜 있는 거지?"});
        talkData.Add(23001, new string[] { "베개에 사진이 끼어있다." });
        talkData.Add(24000, new string[] { "가족앨범이다."});
        talkData.Add(24001, new string[] { "가족앨범이다. 분명 우리 가족사진과 똑같지만….. 내가 빠져있다.",
        "맨 끝에 이건 뭐지?",
        "...",
        "가족앨범 사이에 끼워져 있는 가위를 얻었다." });
        talkData.Add(24002, new string[] { "퍼즐을 찾았다." });

        // 클리어시
        //talkData.Add(15301, new string[] { "퍼즐을 다 풀었다." });
        // 다음 스테이지로 이동


        // 310
        talkData.Add(31001, new string[] { "TV 화면에 콘솔 게임이 떠있다. ", "13년 전에 히트를 쳤던 게임인 것 같다. ", 
            "게임 칩 하나를 얻었다. " }); // TV 최초 상호작용
        talkData.Add(31002, new string[] { "“게임 칩을 더 찾아보자.”" }); // TV 상호작용 (게임칩 x)
        talkData.Add(31003, new string[] { "도시 전체가 보인다. \n메타버스 세계라 그런가, 확실히 비현실적이다. ", "창틀에 아슬아슬하게 놓여있는 게임칩을 찾았다.","왜 여기에 게임칩이 있지…?"});
        talkData.Add(31004, new string[] { "13년 전에 멈춰있다.","달력 사이에서 게임 칩을 찾았다.","왜 여기에 게임칩이 있지…?" });
        talkData.Add(31005, new string[] { "내 모습이다." }); // 거울
        talkData.Add(31006, new string[] { "소파 사이에 시험지와 화이트가 숨겨져 있다.","시험지에 점수를 조작한 흔적이 보인다.\n 동생은 항상 100점을 맞아왔지만, 나는 그러지 못했던 기억이 든다.","— 화이트를 획득했다 —" }); // 소파
        talkData.Add(31007, new string[] { "퍼즐부터 다 풀자" }); // 다른 방 문
        talkData.Add(31008, new string[] { "13년 전에 멈춰있다."});
        talkData.Add(31009, new string[] { "도시 전체가 보인다. \n메타버스 세계라 그런가, 확실히 비현실적이다. " });
        talkData.Add(31011, new string[] { "“게임 칩 3개가 있으니 이제 콘솔 게임을 할 수 있다.”" });
        talkData.Add(31012, new string[] { "여긴 창고 방이다." });
        talkData.Add(31013, new string[] { "아 여긴 부모님 방이었지 ..." });
        talkData.Add(31014, new string[] { "여긴 [YUNOH]의 방이네?" });
        talkData.Add(31015, new string[] { "서재 방을 찾았다!" });
        talkData.Add(31016, new string[] { "콘솔 게임은 다 풀었다.","서재 방을 찾아봐야 한다." });
        talkData.Add(31017, new string[] { "서재 방을 찾아보자." });

        talkData.Add(40001, new string[] { "그 당시에 유행했던 LP판. \nLP판 3개를 찾아서 리듬게임을 해보자." }); //LP판
        talkData.Add(40002, new string[] { "13년 전에 개봉한 영화다. \n포스터 뒤에 LP판이 아슬아슬하게 숨겨져 있다.", "LP판을 얻었다." }); //포스터
        talkData.Add(40003, new string[] { "서랍 속에 여러가지 잡동사니들이 들어있다. \n필기구들이 어지럽혀져 있다.", "필기구들 사이에 파묻혀 있는 LP판을 찾아냈다!" }); //서랍
        talkData.Add(40004, new string[] { "책들 사이에 lp판이 끼어있다. \n손상되지 않았어야 할텐데…." }); //책장(lp판 획득)
        talkData.Add(40005, new string[] { "책상 위에 메모지와 읽다 만 책, \n그리고… 송곳이 보인다. ",
            "책을 읽다 메모를 한 듯 하다.", "대충 아날로그로 되어있는 인간의 기억을 디지털 신호로 바꾼다…는 내용이다. \n내 동생이 좋아할 만한 책이다." , "근데 왜 책상 위에 송곳이 있는 것일까? \n위험하니 치워야지.", 
            "송곳을 챙겼다." }); //책상
        talkData.Add(40006, new string[] { "그 당시에 금지됐던 논란의 금서다.", "조금 불쾌하다. \n어떻게 사람을 데이터화 시키지?"}); //초록색 책
        talkData.Add(40007, new string[] { "응…?" }); //노란책 책장
        talkData.Add(40008, new string[] { "마지막 음악까지 연주했다." }); //클리어

        //510
        talkData.Add(51001, new string[] { "나와 동생이 어릴 적 자주 갖고 놀던 장난감 상자다.",
            "동생은 항상 블록 놀이를 고집했었지만, 난 로봇 싸움 놀이를 좋아했다.",
            "그땐 공룡같은 걸 좋아했었지…","내가 항상 이겼던 기억이 있다." });
        talkData.Add(51002, new string[] { "뭐든 있는 창고의 선반이다.", "YUNOH가 만든 조립 장난감도 있다!" }); //puzzle1
        talkData.Add(51003, new string[] { "해체되어 있는 장난감이 눈에 띈다." }); //puzzle2
        talkData.Add(51004, new string[] { "바닥에 해체된 장난감이 널부러져 있다.", "조립해버려야겠다." }); //puzzle3
        talkData.Add(51005, new string[] { "구급 상자가 두개나 있다. 하나는 상비약이 가지런히 정리되어있다.", "다른 하나는 먹다 만 약들이 가득하다. 동생이 먹었었던 약과 똑같다." }); //FirstAidKit
    }

    public Choice[] GetChoices(int id)
    {
        if (choiceData.ContainsKey(id))
        {
            return choiceData[id];
        }
        return null;
    }

    public string[] GetTalkData(int id)
    {
        if (talkData.ContainsKey(id))
        {
            return talkData[id];
        }
        return null;
    }
}

public class Choice
{
    public string Text { get; private set; }
    public int SceneIndex { get; private set; }

    public Choice(string text, int id)
    {
        Text = text;
        SceneIndex = id;
    }
}