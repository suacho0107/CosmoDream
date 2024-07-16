using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Interact()
    {
        // 상호작용 로직 추가
        Debug.Log(gameObject.name + " 오브젝트와 상호작용");
    }
}
