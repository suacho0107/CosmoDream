using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EndingManager : MonoBehaviour
{
    public Text dialogueText;
    private string[] endingDialogues; // 각 엔딩에 따른 대사 배열
    private int currentDialogueIndex = 0; // 현재 대사 인덱스

    void Start()
    {
        // 현재 씬 이름 가져오기
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 각 씬에 맞는 대사 설정
        switch (currentSceneName)
        {
            case "EndingScene1":
                endingDialogues = new string[] {
                    "[유노? 그런 NPC가 있었나?]",
                    "다른 NPC들은 유노에 대한 기억이 전혀 없었다.",
                    "마치 처음부터 없었던 것처럼.",
                    "[예전에 그런 NPC가 있었는데, 어느 날 갑자기 없어졌어. 다른 NPC들도 기억을 못한다니까?]",
                    "그렇게 유노는 코스모 드림의 괴담이 되었다. "
                };
                break;
            case "EndingScene2":
                endingDialogues = new string[] {
                    "이사한 이후로 윤오는 나에게 없는 사람이나 마찬가지다.",
                    "부모님도 언급을 안하신 지 오래이고, 내 주변 지인 중에서도 윤오를 알고 있는 사람은 이제 없다.",
                    "사람 “윤오”는 이미 모두에게 잊혀졌지만,",
                    "인격데이터 [유노]는 다른 플레이어의 기억 속에서 핵심 NPC로 자리잡고 있을 것이다."
                };
                break;
            case "EndingScene3":
                endingDialogues = new string[] {
                    "그렇게 나는 유노와 이곳 저곳을 놀러 나가게 되었다.",
                    "이사를 마친 후에도 HDM기를 켜서 메타버스 세상을 즐겼다."
                };
                break;
            default:
                endingDialogues = new string[] { "오류" };
                break;
        }

        DisplayCurrentDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceDialogue();
        }
    }

    // 현재 대사를 출력하는 함수
    private void DisplayCurrentDialogue()
    {
        if (currentDialogueIndex < endingDialogues.Length)
        {
            dialogueText.text = endingDialogues[currentDialogueIndex];
        }
    }

    // 다음 대사로 넘어가는 함수
    private void AdvanceDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < endingDialogues.Length)
        {
            DisplayCurrentDialogue();
        }
        else
        {
            Debug.Log("모든 대사가 끝났습니다.");
            FadeManager.instance.ChangeScene("StartMenu");
        }
    }
}