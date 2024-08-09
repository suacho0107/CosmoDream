using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // 상호작용 시 호출되는 기본 메서드 (상속 후 override 가능)
    public virtual void Interact()
    {
        Debug.Log($"{gameObject.name}과 상호작용합니다.");
    }
}

public class NPC : Interactable
{
    // NPC와 상호작용할 경우의 동작 정의
    public override void Interact()
    {
        base.Interact(); // 기본 상호작용 동작도 유지할 수 있습니다.
        Debug.Log("NPC와 상호작용했습니다.");
        // 추가로 NPC와의 상호작용에 따른 동작을 여기에 추가하세요.
    }
}
