using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        int maxIndex = 100;
        isInteracted = new bool[maxIndex];

        // 모든 값을 false로 초기화
        for (int i = 0; i < isInteracted.Length; i++)
        {
            isInteracted[i] = false;
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    #endregion

    public GameObject scanObject;

    TalkManager talkManager;
    BubbleManager bubbleManager;
    ObjData objData;
    Dictionary<string, bool> dialogueExecuted = new Dictionary<string, bool>();

    public int chipsToGive = 1;
    public int gamechips = 0;
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
                    if (!isTalk)
                    {
                        PlayerController playerController = FindObjectOfType<PlayerController>();
                        playerController.SetMove(false);

                        GameObject Edge = GameObject.Find("Right");
                        if (Edge != null)
                        {
                            Edge.SetActive(false);
                        }

                        ypMove YpMove = FindObjectOfType<ypMove>();
                        YpMove.StartMovement();
                        objData.TryChangeId();
                    }
                }
                if (scanObj.CompareTag("GameChip"))
                {
                    // ObjData objData = scanObj.GetComponent<ObjData>();
                    if (objData == null) return;

                    // 게임 칩 추가 및 상호작용 상태 기록
                    if (!isTalk)
                    {
                        Destroy(scanObj); // 오브젝트 파괴
                        gamechips += chipsToGive; // 게임 칩 추가
                        Debug.Log("현재 게임 칩: " + gamechips);

                        // 상호작용 완료로 설정
                        SetInteraction(objData.objIndex);

                        if (gamechips > 2)
                        {
                            UpdateObjectIdsForChips();
                        }
                    }
                }
               
                if (objData.id == 31011 && !isTalk)
                {
                    talkManager.Talk(objData.id); // 대화 시작
                    SceneChange sceneChanger = scanObj.GetComponent<SceneChange>();

                    objData.objectType = ObjData.ObjectType.Talkable; // 대화 가능 상태로 변경
                    Debug.Log($"씬 전환을 시도합니다.");
                    SceneManager.LoadScene("GameController"); // GameController 씬으로 전환
                }
                if (!isTalk)
                {
                    SetInteraction(objData.objIndex);
                    objData.TryChangeId();
                    Debug.Log($"오브젝트 {objData.objIndex} 상호작용 완료");
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
    private void UpdateObjectIdsForChips()
    {
        // 씬에 존재하는 모든 ObjData를 가져옴
        ObjData[] allObjData = FindObjectsOfType<ObjData>();

        foreach (ObjData obj in allObjData)
        {
            // id가 31002인 오브젝트를 찾아 id를 31011로 변경
            if (obj.id == 31002)
            {
                obj.id = 31011;
                Debug.Log($"{obj.gameObject.name}의 ID가 {obj.id}로 변경되었습니다.");
            }
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
}

