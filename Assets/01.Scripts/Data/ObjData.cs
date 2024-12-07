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
        if (GameManager.isInteracted != null &&
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
            else if (id == 31002)
            {
               if (GameManager.instance.gamechips >= 3)
                {
                    id = 31011;
                    objectType = ObjectType.SceneChange;
                }
            }
            else {
            id = secondId;
            Debug.Log($"{gameObject.name} ID가 {id}로 변경"); // 변경 여부 디버깅
             
            if (id == 15103)
                objectType = ObjectType.SceneChange;
            if (id == 21000)
                objectType = ObjectType.Talkable;
            if (id == 51006)
                objectType = ObjectType.Talkable;
            }
        }
    }
}
