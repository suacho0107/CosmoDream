using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public int talkIndex;

    public GameObject scanObject;
    public bool isAction;

    void Start()
    {
        talkPanel.SetActive(false);
    }

    public void Action(GameObject scanObj)
    {
        if (isAction) // ��ȣ�ۿ� ����
        {
            isAction = false;
        }
        else // ��ȣ�ۿ� ����
        {
            isAction = true;
            scanObject = scanObj;
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id, objData.isNpc);
        }
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if(isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }
    }
}
