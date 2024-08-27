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
    public Image portraitImg;

    public GameObject scanObject;
    public bool isAction;

    private ObjData objData;

    void Start()
    {
        talkManager = FindObjectOfType<TalkManager>();
        talkPanel.SetActive(false);
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();

        if (objData == null) return;

    switch (objData.objectType)
    {
        case ObjData.ObjectType.Talkable:
            Talk(objData.id, objData.isNpc);
            break;

        case ObjData.ObjectType.SceneChange:
            Talk(objData.id, objData.isNpc);
            SceneChange sceneChanger = scanObj.GetComponent<SceneChange>();
            if (isAction == false)
                sceneChanger.ChangeScene();
            break;

        case ObjData.ObjectType.ImageDisplay:
            DisplayImage(objData.id); // 이미지 표시를 위한 함수 (아래 설명)
            Talk(objData.id, objData.isNpc);
            if (isAction == false)
                HideImage();
            break;
    }
    
    talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
    void DisplayImage(int id)
    {
        // id에 맞는 이미지를 표시하도록 수정할까 고민중인 부분.. 지금은 gameObject로 직접 할당하여 사용
        ObjData objData = scanObject.GetComponent<ObjData>();
        objData.Display.SetActive(true);
    }

    void HideImage()
    {
        ObjData objData = scanObject.GetComponent<ObjData>();
        objData.Display.SetActive(false);
    }
}
