using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private TalkManager talkManager;
    public BubbleManager bubbleManager;
    PlayerController playerController;

    public GameObject talkPanel;
    public Text UINameText;
    public Text UITalkText;
    public Image portraitImg;

    public GameObject scanObject;
    public bool isTalk; // 대화창 표시중인지 여부
    public string playerName = "player1"; // 나중에 다 구현되면 다른 스크립트에서 받아서 사용

    private ObjData objData;
    public int talkIndex;

    void Start()
    {
        talkManager = FindObjectOfType<TalkManager>();
        playerController = FindObjectOfType<PlayerController>();
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
                if (objData.id == 0) // id가 0인 경우 대화창 없이 바로 이미지 띄움
                {
                    DisplayImage(scanObj);
                    isTalk = true;
                    Invoke("HideImage", 3f);
                    isTalk = false;
                    return;
                }
                else
                {
                    DisplayImage(scanObj);
                    Talk(objData.id, objData.isNpc);
                }
                if (!isTalk)
                {
                    HideImage(scanObj);
                    if (objData.id == 24001)
                    // 한번만 대화 가능하게 하고 싶을 때 여기에 '|| id코드' 적어주시면 됩니다
                        Destroy(scanObj);
                }
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
            playerController.SetMove(true);
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

    public void EnterTalk(GameObject obj)
    {
        ObjData objData = obj.GetComponent<ObjData>();
        int id = objData.id;
        string speakerName;
        string talkData = talkManager.GetTalk(id, talkIndex, out speakerName);

        isTalk = true;
        UINameText.text = "";
        UITalkText.text = talkData;
        talkPanel.SetActive(true);
    }

    void DisplayImage(GameObject obj)
    {
        ObjData objData = obj.GetComponent<ObjData>();
        objData.Display.SetActive(true);
    }

    public void HideImage(GameObject obj)
    {
        ObjData objData = obj.GetComponent<ObjData>();
        objData.Display.SetActive(false);
    }
}
