using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class BreakStage : MonoBehaviour
{
    public Image YUNOH;
    public Sprite BAD;
    public Button Scissors;
    public Button White;
    public Button awl;
    public Button hammer;

    public string nextScene;

    public CanvasGroup fade;

    bool isTransition = false; //중복실행방지
    FadeController fadecontroller;
    CameraShake camerashake;

    private void Start()
    {
        Time.timeScale = 1f;
        fadecontroller = FindObjectOfType<FadeController>();
        camerashake = FindObjectOfType<CameraShake>();

        //버튼 활성화 결정하기
        bool activeScissors = DataController.Instance.gameData._scissors;
        bool activeWhite = DataController.Instance.gameData._white;
        bool activeAwl = DataController.Instance.gameData._awl;
        bool activeHammer = DataController.Instance.gameData._hammer;

        Scissors.interactable = activeScissors;
        White.interactable = activeWhite;
        awl.interactable = activeAwl;
        hammer.interactable = activeHammer;

        //넘어갈 씬 결정
        if (nextScene == "대충 마지막")
        {
            //if(파괴 완){
            //    nextScene = "파괴엔딩";
            //}
            //else if(파괴 완 아님){
            //    nextScene = "덜파괴엔딩";
            //}
            //else if (파괴안함)
            //{
            //    nextScene = "순수엔딩";
            //}
        }
    }

    //버튼에 실행
    public void BreakRule()
    {
        //중복실행방지
        if (isTransition) return;
        isTransition = true;

        //이미지 교체
        YUNOH.sprite = BAD;

        //카메라 흔들림
        camerashake.VibrateForTime(0.05f);

        //파괴카운트
        Count.destroyCount++;
        Debug.Log(Count.destroyCount);

        //게임 정지
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);

        //페이드아웃
        //StartCoroutine(fadecontroller.FadeIn(fade));

        //씬 로드 함수
        //SceneManager.LoadScene(nextScene);
    }
}
