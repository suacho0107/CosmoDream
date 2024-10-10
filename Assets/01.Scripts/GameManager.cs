using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion

    public GameObject scanObject;
    
    TalkManager talkManager;
    BubbleManager bubbleManager;
    ObjData objData;
    public bool isTalk;

    // 게임 진행 변수
    public bool hasScissors = false;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        talkManager = FindObjectOfType<TalkManager>();
        bubbleManager = FindObjectOfType<BubbleManager>();
    }
    
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();

        if (objData == null) return;

        switch (objData.objectType)
        {
            case ObjData.ObjectType.Talkable:
                talkManager.Talk(objData.id);
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
                    talkManager.Talk(objData.id);
                    SceneChange sceneChanger = scanObj.GetComponent<SceneChange>();
                    if (!isTalk)
                        sceneChanger.ChangeScene();
                }
                break;

            case ObjData.ObjectType.ImageDisplay:
                {
                    DisplayImage(scanObj);
                    talkManager.Talk(objData.id);
                }
                if (!isTalk)
                {
                    HideImage(scanObj);
                    if (objData.id == 24001)
                    // 한번만 대화 가능하게 하고 싶을 때 여기에 '|| id코드' 적어주시면 됩니다,
                    // 오브젝트 복붙해서 상호작용 불가능한 오브젝트 하나 남겨두면 돼요
                        Destroy(scanObj);
                }
                break;

            case ObjData.ObjectType.NpcBubble:
                bubbleManager.StartBubbleInteraction(scanObject, objData.id);
                break;
        }
    }

    void DisplayImage(GameObject obj)
    {
        ObjData objData = obj.GetComponent<ObjData>();
        objData.Display.SetActive(true);
    }

    void HideImage(GameObject obj)
    {
        ObjData objData = obj.GetComponent<ObjData>();
        objData.Display.SetActive(false);
        isTalk = false;
    }
}