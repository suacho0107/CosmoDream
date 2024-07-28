using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        // 기본 상호작용 코드
        Debug.Log("상호작용이 가능한 오브젝트 위에서 스페이스 바 입력");
    }
}

public class NPC : Interactable
{
    public override void Interact()
    {
        // NPC와 상호작용할 경우의..
    }
}
