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
        // 110
        talkData.Add(11010, new string[] { "���䡦 ���� ����? ��ó�� ���� �ֳ� ����." });
        talkData.Add(11011, new string[] { "�÷��̾�:���� ���ϴ� �����ҡ�?",
        "�װ͵� �𸣰� ������ ����? ��2�� �λ��� ��� �������ݾƿ�." });
        talkData.Add(11012, new string[] { "�ڽ��𿪿� ���� ���� ȯ���մϴ�!", "��ſ� ��Ÿ���� ��Ȱ �Ǽ���!" });
        talkData.Add(11013, new string[] { "�� ���� ���� ������ ���������ٴ�! ���� �����̾�?",
        "�÷��̾�:...��?", "��? �ƴϱ���. �˼��մϴ�, ģ�� NPC�� �����߾��!" });
        talkData.Add(11004, new string[] { "���� ��¥�� �ð��� �Ȱ���.", "�ǽð� ����ȭ�� �Ǿ��ֳ�.." });
        talkData.Add(11005, new string[] { "�޲ٴ� �λ��� �̷����!" });

        // 120
        talkData.Add(12001, new string[] { "������ ģ���ϰ� �����. �츮 �� ����Ʈ�� ���� �����ϴ�." });
        talkData.Add(12002, new string[] { "(...)" });
        talkData.Add(12003, new string[] { "�������� �����Ͻ� �� ������..." });
        talkData.Add(12004, new string[] { "(...)" });

        // 131
        talkData.Add(13111, new string[] { "YUNOH:�ȳ�! ���ο� ģ����? �� �̰��� ������ �����.",
        "�÷��̾�:(�ͼ��� ������ ���.) �ȳ�. �̰��� ó�� �ͺôµ� ������ �ϸ� ������?",
        "YUNOH:���� �ȳ����ٰ�! �ϴ� �� ����Ʈ���� �����غ���?"});

        // 132
        talkData.Add(13211, new string[] { "YUNOH:����� �帲 ����Ʈ��. ���� ������!",
        "�÷��̾�:(����Ʈ �̸� ��¥ ���� �����١�.) ��ġ�� �� ����.",
        "YUNOH:��ġ? ���� ���Ⱑ ���� ���� ������ �� �ž�.", "YUNOH:... ...", "YUNOH:��...", "�÷��̾�:�� �׷�?",
        "YUNOH:�� ���� ��� �̾���. \n���ο� ������ �� ���� ������ �� ������ �����߰ڴ�! �� �� ��������ٰ�!"});

        // 141
        talkData.Add(14111, new string[] { "�÷��̾�:(�츮 ���� ������ ���� �� ���� �����.)"});
        talkData.Add(14102, new string[] { "�� ���� �ƴϾ�!"});
        talkData.Add(14103, new string[] { "� �� �츮 ���� �־��� ���Ķ� �Ȱ��� �����."});
        talkData.Add(14304, new string[] { "���� ��ü�� ���δ�. ��Ÿ���� ����� �׷���, Ȯ���� ���������̾�."});

        // 151
        talkData.Add(15111, new string[] { "YUNOH:���Ⱑ �� ���̾�! ���� ���� ���� ������� �����ϱ� �ɽ����� �����ž�.",
        "������� Ǯ���? (������)" });
        talkData.Add(15102, new string[] { "YUNOH:������ Ǯ�� ��!"});

        // 152
        talkData.Add(15201, new string[] { "�÷��̾�:�θ���̶� �ʾ�?", "YUNOH:��!", "�÷��̾�:�� Ȥ�� ������ ����?",
        "YUNOH:��... �۽�?" });
        talkData.Add(15202, new string[] { "YUNOH:�װ� �ٸ� NPC���̶� ���� ��ũ�����̾�!" });

        // 153
        talkData.Add(15301, new string[] { "�̰� �� �ϳ��� ��� ������ ��ƴ���� �̾��ָ� ��. ����?" });


               
        //portraitData.Add(1000 + 0,);
        //portraitData.Add(1000 + 1,);
        //portraitData.Add(1000 + 2,);
        //portraitData.Add(1000 + 3,);

        // 210~240
        talkData.Add(21001, new string[] { "ȭ��� ���� ���� ������ �����ִ�." });
        talkData.Add(23001, new string[] { "������ ������ �����ִ�." });
        talkData.Add(24001, new string[] { "�����ٹ��̴�. �и� �츮 ���������� �Ȱ�������.. ���� �����ִ�.",
        "�� ���� �̰� ����?",
        "...",
        "�����ٹ� ���̿� ������ �ִ� ������ �����." });
        talkData.Add(24002, new string[] { "������ ã�Ҵ�." });


        talkData.Add(40001, new string[] { "�� ��ÿ� �����ߴ� LP��. \nLP�� 3���� ã�Ƽ� ��������� �غ���." }); //LP��
        talkData.Add(40002, new string[] { "13�� ���� ������ ��ȭ��. \n������ �ڿ� LP���� �ƽ��ƽ��ϰ� ������ �ִ�.", "LP���� �����." }); //������
        talkData.Add(40003, new string[] { "���� �ӿ� �������� �⵿��ϵ��� ����ִ�. \n�ʱⱸ���� ���������� �ִ�.", "�ʱⱸ�� ���̿� �Ĺ��� �ִ� LP���� ã�Ƴ´�!" }); //����
        talkData.Add(40004, new string[] { "å�� ���̿� lp���� �����ִ�. \n�ջ���� �ʾҾ�� ���ٵ���." }); //å��(lp�� ȹ��)
        talkData.Add(40005, new string[] { "å�� ���� �޸����� �д� �� å, \n�׸��� �۰��� ���δ�. ",
                "å�� �д� �޸� �� �� �ϴ�.", "���� �Ƴ��α׷� �Ǿ��ִ� �ΰ��� ����� ������ ��ȣ�� �ٲ۴١��� �����̴�. \n�� ������ ������ ���� å�̴�." , "�ٵ� �� å�� ���� �۰��� �ִ� ���ϱ�? \n�����ϴ� ġ������.", 
                "�۰��� ì���." }); //å��
        talkData.Add(40006, new string[] { "�� ��ÿ� �����ƴ� ����� �ݼ���.", "���� �����ϴ�. \n��� ����� ������ȭ ��Ű��?", "�������� �߰��ߴ�." }); //�ʷϻ� å
        talkData.Add(40007, new string[] { "����?" }); //���å å��
        talkData.Add(40008, new string[] { "������ ���Ǳ��� �����ߴ�." }); //Ŭ����

    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id) || talkIndex >= talkData[id].Length)
        {
            return null;
        }

        var talkEntry = talkData[id][talkIndex];
        var parts = talkEntry.Split(':');

        if (parts.Length == 2)
        {
            return parts[1]; // ��縸 ��ȯ
        }
        else
        {
            return talkEntry; // �̸� ���� ��� ��ȯ��
        }
    }

    public string GetTalk(int id, int talkIndex, out string speakerName)
    {
        if (!talkData.ContainsKey(id) || talkIndex >= talkData[id].Length)
        {
            speakerName = "";
            return null;
        }

        var talkEntry = talkData[id][talkIndex];
        var parts = talkEntry.Split(':');

        if (parts.Length == 2)
        {
            speakerName = parts[0];
            return parts[1];
        }
        else
        {
            speakerName = "";
            return talkEntry;
        }
    }
}
