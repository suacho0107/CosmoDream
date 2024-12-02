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
    CameraShake camerashake;

    private void Start()
    {
        Time.timeScale = 1f;
        fadecontroller = FindObjectOfType<FadeController>();
        camerashake = FindObjectOfType<CameraShake>();

        //��ư Ȱ��ȭ �����ϱ�
        bool activeScissors = DataController.Instance.gameData._scissors;
        bool activeWhite = DataController.Instance.gameData._white;
        bool activeAwl = DataController.Instance.gameData._awl;
        bool activeHammer = DataController.Instance.gameData._hammer;

        Scissors.interactable = activeScissors;
        White.interactable = activeWhite;
        awl.interactable = activeAwl;
        hammer.interactable = activeHammer;

        //�Ѿ �� ����
        if (nextScene == "���� ������")
        {
            //if(�ı� ��){
            //    nextScene = "�ı�����";
            //}
            //else if(�ı� �� �ƴ�){
            //    nextScene = "���ı�����";
            //}
            //else if (�ı�����)
            //{
            //    nextScene = "��������";
            //}
        }
    }

    //��ư�� ����
    public void BreakRule()
    {
        //�ߺ��������
        if (isTransition) return;
        isTransition = true;

        //�̹��� ��ü
        YUNOH.sprite = BAD;

        //ī�޶� ��鸲
        camerashake.VibrateForTime(0.05f);

        //�ı�ī��Ʈ
        Count.destroyCount++;
        Debug.Log(Count.destroyCount);

        //���� ����
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);

        //���̵�ƿ�
        //StartCoroutine(fadecontroller.FadeIn(fade));

        //�� �ε� �Լ�
        //SceneManager.LoadScene(nextScene);
    }
}
