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

   DataController datacontroller;

    private void Start()
    {
        datacontroller = DataController.Instance;
    }

    public void ClearStage()
    {
        Debug.Log($"���� ����: puzzle1Clear={datacontroller.gameData.puzzle1Clear}, puzzle2Clear={datacontroller.gameData.puzzle2Clear}, puzzle3Clear={datacontroller.gameData.puzzle3Clear}");

        if (datacontroller.gameData.puzzle1Clear &&
            datacontroller.gameData.puzzle2Clear &&
            datacontroller.gameData.puzzle3Clear)
        {
            Debug.Log("��� ���� �Ϸ� Ȯ�ε�. ���� ���������� �̵�.");

            //Ŭ���� üũ�� ���� = true;
            //���� ���������� �Ѿ������ ^�� �ٽ� false�� ����

            //���� ���� ���� �� ���۵� �������� ����
            datacontroller.gameData.nowStg++;

            //�÷��̾� ��ġ �ʱⰪ �־��ֱ�
            PlayerPosData.pos = initPosition;
            PlayerPosData.romInx = 0;

            //���ε�(NextStgScene)
            FadeManager.instance.ChangeScene(NextStgScene);
            GameManager.instance.isSecondLoad = false;
            Debug.Log("���� ���������� ���ư��ϴ�. (" + NextStgScene + ")");

            //���� Ŭ���� ���� �ǵ�����
            datacontroller.gameData.puzzle1Clear = false;
            datacontroller.gameData.puzzle2Clear = false;
            datacontroller.gameData.puzzle3Clear = false;
        }

        else
        {
            //���ε�(NowStgScene)
            FadeManager.instance.ChangeScene(NowStgScene);
            GameManager.instance.isSecondLoad = true;

            Debug.Log("���� �Ϸ���� ����, ���� ���������� ���ư��ϴ�. (" + NowStgScene + ")");
        }
    }
}
