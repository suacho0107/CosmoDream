using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Singleton
    void Awake()
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
    

    public GameObject scanObject;
    
    TalkManager talkManager;
    BubbleManager bubbleManager;
    ObjData objData;
    public bool isTalk = false;

    public int chipsToGive = 1;
    public int gamechips = 0;
    public bool hasScissors = false;

    public bool isSecondLoad = false;
    string lastSceneName = "";
    public bool startDialogHasRun = false; // 대화 실행 여부
    public int completedPuzzles = 0;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        talkManager = FindObjectOfType<TalkManager>();
        bubbleManager = FindObjectOfType<BubbleManager>();
        isTalk = false;

        if (scene.name == "1-5 room" && lastSceneName == "1-6 Puzzle4" // 예: 1-6이 퍼즐 씬이고 1-5가 돌아오는 씬
        || scene.name == "2" && lastSceneName != "1-5 room" && lastSceneName != "2" )
        {
            startDialogHasRun = true;  // 퍼즐 씬에서 돌아왔을 때는 StartDialogue를 실행하지 않음
            isSecondLoad = true;
        }
        else
        {
            startDialogHasRun = false;  // 새로운 스테이지에서는 StartDialogue를 다시 실행하도록 초기화
        }
        
        // 현재 씬 이름을 lastSceneName에 저장
        lastSceneName = scene.name;
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

                if (scanObj.CompareTag("GameChip"))
                {
                    gamechips += chipsToGive;  // 게임 칩 추가
                    Debug.Log("현재 게임 칩: " + gamechips);
                    Destroy(scanObj);  // 오브젝트 파괴
                }
                else if (scanObj.CompareTag("LineGame"))
                {
                    if (gamechips >= 1 && !isTalk)
                    {
                        Debug.Log("씬 전환");//씬 전환
                        gamechips -= 1;
                        Destroy(scanObj);
                        SceneChange sceneChanger = scanObj.GetComponent<SceneChange>();
                        if (!isTalk)
                            sceneChanger.ChangeScene();
                    }
                }
                break;

            case ObjData.ObjectType.SceneChange:
                if (objData.id == 0) // id가 0인 경우 대화창 없이 바로 씬 전환
                {
                    SceneChange sceneChanger = scanObj.GetComponent<SceneChange>();
                    sceneChanger.ChangeScene();
                return;
                }
                else if (objData.id == 21001)
                {
                    talkManager.Talk(objData.id);
                    SceneChange sceneChanger = scanObj.GetComponent<SceneChange>();
                    if (!isTalk)
                    {
                        Destroy(scanObj);
                        sceneChanger.ChangeScene();
                    }
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
    }
    #endregion
}