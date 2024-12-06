using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public int id;
    public int objIndex; // 고유인덱스

    public bool isNpc;
    public ObjectType objectType;
    public GameObject Display;

    public bool idChange = false; // 대사가 달라지는 오브젝트만 체크해주세요.
    public int secondId = 0; // 변경할 아이디를 입력해주세요. (인스펙터 창에서)

    void Start()
    {
        DataController datacontroller = FindObjectOfType<DataController>();
        if (datacontroller != null &&
            datacontroller.gameData.puzzle1Clear &&
            datacontroller.gameData.puzzle2Clear &&
            datacontroller.gameData.puzzle3Clear)
        {
            if (id == 20000) // 퍼즐 다 풀었고 아이디가 2000이면 아이디 바꾸기
            {
                objectType = ObjectType.SceneChange;
                id = secondId;
            }
        }

        else if (GameManager.isInteracted != null &&
            objIndex >= 0 &&
            objIndex < GameManager.isInteracted.Length &&
            GameManager.isInteracted[objIndex])
        {
            TryChangeId();
        }

        if (Display == null)
            return;
    }

    public enum ObjectType
    {
        Talkable,
        NpcBubble,
        SceneChange,
        ImageDisplay,
        None
    }

    public void TryChangeId()
    {
        if (idChange)
        {
            if (id == 13211)
                objectType = ObjectType.None;
            else if (id == 15103)
                objectType = ObjectType.SceneChange;
            else if (id == 21000)
                objectType = ObjectType.Talkable;
        }
        id = secondId;
        Debug.Log($"{gameObject.name} ID가 {id}로 변경"); // 변경 여부 디버깅
    }
}