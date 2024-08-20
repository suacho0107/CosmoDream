using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    // Dictionary<int, Sprite> portraitData;

    // public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        // portraitData = new Dictionary<int, Sprite>();
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
        talkData.Add(100, new string[] { "���� ��¥�� �ð��� �Ȱ���.", "�ǽð� ����ȭ�� �Ǿ��ֳ�.." });
        talkData.Add(200, new string[] { "�޲ٴ� �λ��� �̷����!" });

        //portraitData.Add(1000 + 0,);
        //portraitData.Add(1000 + 1,);
        //portraitData.Add(1000 + 2,);
        //portraitData.Add(1000 + 3,);

        // �������� 2
        talkData.Add(211, new string[] { "ȭ��� ���� ���� ������ �����ִ�." });
        talkData.Add(231, new string[] { "������ ������ �����ִ�." });
        talkData.Add(241, new string[] { "�����ٹ��̴�. �и� �츮 ���������� �Ȱ�������.. ���� �����ִ�.",
        "�� ���� �̰� ����?",
        "...",
        "�����ٹ� ���̿� ������ �ִ� ������ �����." });
        talkData.Add(242, new string[] { "������ ã�Ҵ�." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
