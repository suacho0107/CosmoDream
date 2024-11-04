using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public int id;
    public bool isNpc;
    public ObjectType objectType;
    public GameObject Display;
    
    public bool idChange = false; // 대사가 달라지는 오브젝트만 체크해주세요.
    public int secondId = 0; // 바꿀 대사가 들어있는 아이디를 입력해주세요. (인스펙터 창에서)

    void Start() {
        if (Display == null)
        return;
        else
        Display.SetActive(false);
    }

    public enum ObjectType
    {
        Talkable,
        NpcBubble,
        SceneChange,
        ImageDisplay
    }

    private void OnEnable()
    {
        TryChangeId();
    }

    public void TryChangeId()
    {
        if(idChange && GameManager.instance != null
        && GameManager.instance.isSecondLoad )
        {
            id = secondId;
            Debug.Log($"{gameObject.name}의 ID가 {id}로 변경되었습니다.");

            if (id == 15103)
            {
                objectType = ObjectType.SceneChange;
            }
        }
    }
}
