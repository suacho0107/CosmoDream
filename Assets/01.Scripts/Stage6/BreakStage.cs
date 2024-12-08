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
    CanvasShake canvasShake;
    DataController datacontroller;

    private void Start()
    {
        Time.timeScale = 1f;
        fadecontroller = FindObjectOfType<FadeController>();
        canvasShake = FindObjectOfType<CanvasShake>();
        datacontroller = FindObjectOfType<DataController>();

        //버튼 활성화 결정하기
        //bool activeScissors = datacontroller.gameData._scissors;
        //bool activeWhite = datacontroller.gameData._white;
        //bool activeAwl = datacontroller.gameData._awl;
        //bool activeHammer = datacontroller.gameData._hammer;

        //Scissors.interactable = activeScissors;
        //White.interactable = activeWhite;
        //awl.interactable = activeAwl;
        //hammer.interactable = activeHammer;

        //넘어갈 씬 결정

    }

    //버튼에 실행
    public void BreakRule()
    {
        //중복실행방지
        if (isTransition) return;
        isTransition = true;

        StartCoroutine(BreakReference());
    }

    IEnumerator BreakReference()
    {
        //이미지 교체
        YUNOH.sprite = BAD;

        //카메라 흔들림
        //canvasShake.VibrateForTime(0.5f);

        //오디오 정지

        //필요시 쿵 하는 오디오 삽입

        //파괴카운트
        Count.destroyCount++;
        Debug.Log(Count.destroyCount);

        //게임 정지
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(1f);

        //씬 로드 함수
        SceneManager.LoadScene(nextScene);

    }

    public void DontBreak()
    {
        if (nextScene == "Route")
        {
            if (Count.destroyCount == 4)
            {
                nextScene = "00.Scenes/Stage6/RouteDestroy";
            }
            else if (Count.destroyCount == 0)
            {
                nextScene = "00.Scenes/Stage6/RouteDontDestroy";
            }
            else
            {
                nextScene = "00.Scenes/Stage6/RouteNormal";
            }
        }

        //StartCoroutine(fadecontroller.FadeIn(fade));
        SceneManager.LoadScene(nextScene);
    }
}
