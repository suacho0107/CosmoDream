using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // ��ȣ�ۿ� �� ȣ��Ǵ� �⺻ �޼��� (��� �� override ����)
    public virtual void Interact()
    {
        Debug.Log($"{gameObject.name}�� ��ȣ�ۿ��մϴ�.");
    }
}

public class NPC : Interactable
{
    // NPC�� ��ȣ�ۿ��� ����� ���� ����
    public override void Interact()
    {
        base.Interact(); // �⺻ ��ȣ�ۿ� ���۵� ������ �� �ֽ��ϴ�.
        Debug.Log("NPC�� ��ȣ�ۿ��߽��ϴ�.");
        // �߰��� NPC���� ��ȣ�ۿ뿡 ���� ������ ���⿡ �߰��ϼ���.
    }
}
