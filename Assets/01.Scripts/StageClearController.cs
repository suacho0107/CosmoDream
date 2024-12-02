using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̾��Ű�� �� ������Ʈ ����� �� ��ũ��Ʈ �־��ּ���
//public �� ���� �������ֱ�

public class StageClearController : MonoBehaviour
{
    public string NowStgScene; //�������� 2�̸� ��������2 ���̸� �ֱ�
    public string NextStgScene; //�������� 2�̸� ��������3 ���̸� �ֱ�
    public Vector3 initPosition = new Vector3(-9.3f, -1.6f, 0); //�������� 2�̸� ��������3 �ʱ� �÷��̾� ��ġ �ֱ�

    //DataController datacontroller;

    private void Start()
    {
        //datacontroller = FindObjectOfType<DataController>();
    }

    private void Update()
    {
        if(DataController.Instance.gameData.puzzle1Clear&& 
            DataController.Instance.gameData.puzzle2Clear&& 
            DataController.Instance.gameData.puzzle3Clear)
        {
            //���� Ŭ���� ���� �ǵ�����
            DataController.Instance.gameData.puzzle1Clear = false;
            DataController.Instance.gameData.puzzle2Clear = false;
            DataController.Instance.gameData.puzzle3Clear = false;

            //���� ���� ���� �� ���۵� �������� ����
            DataController.Instance.gameData.nowStg++;

            //�÷��̾� ��ġ �ʱⰪ �־��ֱ�
            PlayerPosData.pos = initPosition;

            //���ε�(NextStgScene)
            FadeManager.instance.ChangeScene(NextStgScene);
            GameManager.instance.isSecondLoad = true;
        }
        else
        {
            //���ε�(NowStgScene)
            FadeManager.instance.ChangeScene(NowStgScene);
            GameManager.instance.isSecondLoad = true;
        }
    }
}
