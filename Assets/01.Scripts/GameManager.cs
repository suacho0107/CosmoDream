using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public BubbleManager bubbleManager;
    public GameObject talkPanel;
    public Text UINameText;
    public Text UITalkText;
    public Image portraitImg;

    public GameObject scanObject;
    public bool isTalk; // 대화창 표시중인지 여부
    public string playerName; // 나중에 다 구현되면 다른 스크립트에서 받아서 사용

    private ObjData objData;
    public int talkIndex;

    void Start()
    {
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
                if (objData.id == 0) // id가 0인 경우 대화창 없이 바로 씬 전환
                {
                SceneChange sceneChanger = scanObj.GetComponent<SceneChange>();
                sceneChanger.ChangeScene();
                return;
                }
                else
                {
                Talk(objData.id, objData.isNpc);
                SceneChange sceneChanger = scanObj.GetComponent<SceneChange>();
                if (!isTalk)
                    sceneChanger.ChangeScene();
                }
                break;

            case ObjData.ObjectType.ImageDisplay:
                DisplayImage(objData.id);
                Talk(objData.id, objData.isNpc);
                if (!isTalk)
                    HideImage();
                break;

            case ObjData.ObjectType.NpcBubble:
                bubbleManager.StartBubbleInteraction(scanObject, objData.id);
                break;
        }
    
        talkPanel.SetActive(isTalk);
    }

    void Talk(int id, bool isNpc)
    {
        string speakerName;
        string talkData = talkManager.GetTalk(id, talkIndex, out speakerName);

        if (talkData == null)
        {
            isTalk = false;
            talkIndex = 0;
            return;
        }

        UINameText.text = (speakerName == "플레이어") ? playerName : speakerName;
        UITalkText.text = talkData;

        // portraitImg.sprite = talkManager.GetPortrait(id,int.Parse(talkData.Split(':')[1]));
        // portraitImg.color = isNpc ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);

        isTalk = true;
        talkIndex++;
    }

    void DisplayImage(int id)
    {
        // id에 맞는 이미지를 표시하도록 수정할까 고민중인 부분.. 지금은 gameObject로 직접 할당하여 사용
        objData.Display.SetActive(true);
    }

    void HideImage()
    {
        objData.Display.SetActive(false);
    }
}
