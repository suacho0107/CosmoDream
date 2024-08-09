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
        if (isMove) // 상호작용 시작
        {
            isMove = false;
            scanObject = scanObj;
            talkText.text = scanObject.name + "과 상호작용";
            talkPanel.SetActive(true);
        }
        else // 상호작용 종료
        {
            isMove = true;
            talkPanel.SetActive(false);
        }
    }
}
