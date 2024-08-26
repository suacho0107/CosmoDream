using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // npc
        talkData.Add(11011, new string[] { "���� ���ϴ� �����ҡ�?",
        "�װ͵� �𸣰� ������ ����? ��2�� �λ��� ��� �������ݾƿ�." });
        talkData.Add(11012, new string[] { "�ڽ��𿪿� ���� ���� ȯ���մϴ�!", "��ſ� ��Ÿ���� ��Ȱ �Ǽ���!" });
        talkData.Add(11013, new string[] { "�� ���� ���� ������ ���������ٴ�! ���� �����̾�?",
        "��?", "��? �ƴϱ���. �˼��մϴ�, ģ�� NPC�� �����߾��!" });
        talkData.Add(11004, new string[] { "���� ��¥�� �ð��� �Ȱ���.", "�ǽð� ����ȭ�� �Ǿ��ֳ�.." });
        talkData.Add(11005, new string[] { "�޲ٴ� �λ��� �̷����!" });

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

        talkData.Add(12011, new string[] { "������ ģ���ϰ� �����. �츮 �� ����Ʈ�� ���� �����ϴ�." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
