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

    bool isTransition = false; //�ߺ��������
    FadeController fadecontroller;
    CanvasShake canvasShake;
    DataController datacontroller;

    private void Start()
    {
        Time.timeScale = 1f;
        fadecontroller = FindObjectOfType<FadeController>();
        canvasShake = FindObjectOfType<CanvasShake>();
        datacontroller = FindObjectOfType<DataController>();

        //��ư Ȱ��ȭ �����ϱ�
        //bool activeScissors = datacontroller.gameData._scissors;
        //bool activeWhite = datacontroller.gameData._white;
        //bool activeAwl = datacontroller.gameData._awl;
        //bool activeHammer = datacontroller.gameData._hammer;

        //Scissors.interactable = activeScissors;
        //White.interactable = activeWhite;
        //awl.interactable = activeAwl;
        //hammer.interactable = activeHammer;

        //�Ѿ �� ����

    }

    //��ư�� ����
    public void BreakRule()
    {
        //�ߺ��������
        if (isTransition) return;
        isTransition = true;

        StartCoroutine(BreakReference());
    }

    IEnumerator BreakReference()
    {
        //�̹��� ��ü
        YUNOH.sprite = BAD;

        //ī�޶� ��鸲
        //canvasShake.VibrateForTime(0.5f);

        //����� ����

        //�ʿ�� �� �ϴ� ����� ����

        //�ı�ī��Ʈ
        Count.destroyCount++;
        Debug.Log(Count.destroyCount);

        //���� ����
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(1f);

        //�� �ε� �Լ�
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
