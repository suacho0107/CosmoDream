using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> upperLaneNotes = new List<GameObject>();
    public List<GameObject> lowerLaneNotes = new List<GameObject>();

    // 0: Perfect, 1: Great, 2: Miss
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;

    private EffectManager effectManager;

    void Start()
    {
        effectManager = FindObjectOfType<EffectManager>();
        //Ÿ�̹� �ڽ� perfect->miss
        timingBoxs = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            float rectCenterX = timingRect[i].localPosition.x;
            timingBoxs[i].Set(rectCenterX - timingRect[i].rect.width / 2, rectCenterX + timingRect[i].rect.width / 2);
        }
    }

    void Update()
    {
        CheckMiss("upper");
        CheckMiss("lower");
    }

    public void CheckTiming(string lane)
    {
        Debug.Log("�Լ� ����");
        // �ش� ������ ��Ʈ ����Ʈ ���� (����, �Ʒ���)
        List<GameObject> noteList = lane == "upper" ? upperLaneNotes : lowerLaneNotes;

        for (int i = 0; i < noteList.Count; i++)
        {
            float t_notePosX = noteList[i].transform.localPosition.x;
            
            // ������ Perfect -> Great -> Miss ������ �̷���� (���� �������� �˻�)
            for (int x = 0; x < timingBoxs.Length - 1; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    noteList[i].GetComponent<Note>().HideNote(); // ��Ʈ ����
                    noteList.RemoveAt(i); // ����Ʈ���� ����
                    effectManager.JudgementEffect(x, lane); // x�� �ش��ϴ� ���� ��� (0: Perfect, 1: Great, 2: Miss)

                    Debug.Log("�����");
                    return;
                }
            }
        }

        // �ش� ������ ���� ��� Miss ó��
        //effectManager.JudgementEffect(timingBoxs.Length);
    }

    public void CheckMiss(string lane)
    {
        List<GameObject> noteList = lane == "upper" ? upperLaneNotes : lowerLaneNotes;

        for (int i = 0; i < noteList.Count; i++)
        {
            float t_notePosX = noteList[i].transform.localPosition.x;

            // Miss ������ �ش��ϸ� ��� Miss ó��
            if (timingBoxs[2].x <= t_notePosX && t_notePosX <= timingBoxs[2].y) // Miss ������ �ش�
            {
                noteList.RemoveAt(i); // ����Ʈ���� ����
                effectManager.JudgementEffect(2, lane); // Miss ���� ���
                Debug.Log("Miss ó����");
                return;
            }
        }
    }
}
