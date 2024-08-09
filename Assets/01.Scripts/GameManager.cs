using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isMove;

    void Start()
    {
        talkPanel.SetActive(false);
    }

    public void Action(GameObject scanObj)
    {
        if (isMove) // ��ȣ�ۿ� ����
        {
            isMove = false;
            scanObject = scanObj;
            talkText.text = scanObject.name + "�� ��ȣ�ۿ�";
            talkPanel.SetActive(true);
        }
        else // ��ȣ�ۿ� ����
        {
            isMove = true;
            talkPanel.SetActive(false);
        }
    }
}
