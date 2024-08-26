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

    public GameObject bubbleCanvas;
    public GameObject bubblePrefab;
    private Queue<GameObject> bubblePool = new Queue<GameObject>();

    public GameObject scanObject;
    public bool isAction;

    private ObjData objData;

    void Start()
    {
        talkPanel.SetActive(false);
        InitBubblePool(5);
    }

    void InitBubblePool(int count)
    {
        Transform bubbleCanvasTransform = bubbleCanvas.transform;

        for (int i = 0; i < count; i++)
        {
            var bubble = Instantiate(bubblePrefab);
            bubble.transform.SetParent(bubbleCanvasTransform, false);
            bubble.SetActive(false);
            bubblePool.Enqueue(bubble);
        }
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
        case ObjData.ObjectType.NpcBubble:
            Bubble(objData.id);
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

    void Bubble(int id)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        // 프리팹을 풀에서 가져옴
        var bubble = bubblePool.Dequeue();
        bubble.SetActive(true);
        // 텍스트 설정
        var bubbleText = bubble.GetComponentInChildren<Text>();
        bubbleText.text = talkData;
        // 말풍선을 NPC의 자식으로 설정
        bubble.transform.SetParent(scanObject.transform);
        // 말풍선 위치 조정
        var bubbleTransform = bubble.GetComponent<RectTransform>();
        bubbleTransform.localPosition = new Vector3(0, 1, 0);  // NPC 위에 말풍선 위치 설정

        talkIndex++;

        // 일정 시간 후 말풍선 비활성화, 풀에 반환
        // StartCoroutine(HideBubble(bubble, 3f));
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
