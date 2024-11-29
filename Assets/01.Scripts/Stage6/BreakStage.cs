using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class BreakStage : MonoBehaviour
{
    public Button Scissors;
    public Button White;
    public Button awl;
    public Button hammer;

    public string nextScene;

    public CanvasGroup fade;

    bool isTransition = false; //�ߺ��������
    FadeController fadecontroller;

    private void Start()
    {
        fadecontroller = FindObjectOfType<FadeController>();

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

        //�ı�ī��Ʈ


        //���� ����
        Time.timeScale = 0;

        //���̵�ƿ�
        fadecontroller.FadeOut(fade);

        Time.timeScale = 1;         //�̰� ������? �ȱ������� �ڷ�ƾ���� ����
        //�� �ε� �Լ�
        SceneManager.LoadScene(nextScene);
    }
}
