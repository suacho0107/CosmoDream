using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSingleTone : MonoBehaviour
{
    // �̱��� �ν��Ͻ��� ������ ���� ����
    public static PauseSingleTone pauseInstance { get; private set; }

    void Awake()
    {
        // �ν��Ͻ��� �̹� �����ϸ� �ڽ��� �ı�
        if (pauseInstance != null && pauseInstance != this)
        {
            Debug.Log($"�ߺ��� �̱��� �߰�: {gameObject.name}. ���� ������Ʈ�� �����ϰ� �ڽ��� �ı��մϴ�.");
            Destroy(gameObject);
            return;
        }

        // �̱��� �ν��Ͻ� ����
        pauseInstance = this;

        // �� ���� �ÿ��� �ı����� �ʵ��� ����
        DontDestroyOnLoad(gameObject);
    }
}
