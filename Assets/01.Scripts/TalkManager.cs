using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        // npc
        talkData.Add(1000, new string[] { "�װ͵� �𸣰� ������ ����? ��2�� �λ��� ��� �������ݾƿ�." });
        talkData.Add(2000, new string[] { "�ڽ��𿪿� ���� ���� ȯ���մϴ�! ��ſ� ��Ÿ���� ��Ȱ �Ǽ���!" });
        talkData.Add(3000, new string[] { "�� ���� ���� ������ ���������ٴ�! ���� �����̾�?",
            "��? �ƴϱ���. �˼��մϴ�, ģ�� NPC�� �����߾��!" });

        // obj
        talkData.Add(100, new string[] { "���� �ð��� ��¥�� �Ȱ���.", "�ǽð� ����ȭ�� �Ǿ��ֳ�����." });
        talkData.Add(200, new string[] { "�޲ٴ� �λ��� �̷����!" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
