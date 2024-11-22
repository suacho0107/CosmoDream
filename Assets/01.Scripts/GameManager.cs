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

        int maxIndex = 30;
        isInteracted = new bool[maxIndex];

        // 모든 값을 false로 초기화
        for (int i = 0; i < isInteracted.Length; i++)
        {
            isInteracted[i] = false;
        }
    }
    

    public GameObject scanObject;
    
    TalkManager talkManager;
    BubbleManager bubbleManager;
    ObjData objData;
    Dictionary<string, bool> dialogueExecuted = new Dictionary<string, bool>();

    public int chipsToGive = 1;
    public int gamechips = 0;
    public bool hasScissors = false;

    public bool isTalk = false;
    public bool isSecondLoad = false;
    public int completedPuzzles = 0;

    public static bool[] isInteracted;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        talkManager = FindObjectOfType<TalkManager>();
        bubbleManager = FindObjectOfType<BubbleManager>();
        
        isTalk = false;
    }
    
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();

        if (objData == null) return;

        switch (objData.objectType)
        {
            case ObjData.ObjectType.None:
                break;
            
            case ObjData.ObjectType.Talkable:
                talkManager.Talk(objData.id);
                if (objData.id == 13111 || objData.id == 13211)
                {
                    if(!isTalk)
                    {
                        PlayerController playerController = FindObjectOfType<PlayerController>();
                        playerController.SetMove(false);
                        ypMove YpMove = FindObjectOfType<ypMove>();
                        YpMove.StartMovement();
                        objData.TryChangeId();
                    }
                }
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
                else
                {
                    talkManager.Talk(objData.id);

                    SceneChange sceneChanger = scanObj.GetComponent<SceneChange>();
                    if (!isTalk)
                    {   
                        sceneChanger.ChangeScene();
                        SetInteraction(objData.objIndex);
                        Debug.Log($"오브젝트 {objData.objIndex} 상호작용 완료");
                    }
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
                    SetInteraction(objData.objIndex);
                    objData.TryChangeId();
                    Debug.Log($"오브젝트 {objData.objIndex} 상호작용 완료");
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

    public bool HasDialogueRun(string sceneName)
    {
        return dialogueExecuted.ContainsKey(sceneName) && dialogueExecuted[sceneName];
    }

    public void SetDialogueRun(string sceneName)
    {
        if (!dialogueExecuted.ContainsKey(sceneName))
        {
            dialogueExecuted[sceneName] = true;
        }
    }

    public void SetInteraction(int index)
    {
        if (index >= 0 && index < isInteracted.Length)
        {
            isInteracted[index] = true;
            Debug.Log($"SetInteraction 호출됨: 인덱스 {index} 상호작용 완료로 설정됨");
        }
    }

    #endregion
}