using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        // �⺻ ��ȣ�ۿ� �ڵ�
        Debug.Log("��ȣ�ۿ��� ������ ������Ʈ ������ �����̽� �� �Է�");
    }
}

public class NPC : Interactable
{
    public override void Interact()
    {
        // NPC�� ��ȣ�ۿ��� �����..
    }
}
