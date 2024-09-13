using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> upperLaneNotes = new List<GameObject>();
    public List<GameObject> lowerLaneNotes = new List<GameObject>();

    [SerializeField] Collider2D perfectCollider = null;
    [SerializeField] Collider2D greatCollider = null;
    [SerializeField] Collider2D missCollider = null;

    // ���� ������ ��Ʈ�� ������ ����Ʈ (�浹 �� ��� ���� ��Ʈ)
    private List<GameObject> availableNotes = new List<GameObject>();

    private EffectManager effectManager;

    void Start()
    {
        effectManager = FindObjectOfType<EffectManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            availableNotes.Add(collision.gameObject);  // ��Ʈ �߰� (���� ��� ��)
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note") && collision == missCollider)
        {
            GameObject note = collision.gameObject;
            ProcessJudgement(note, 2); // Miss ����
        }
    }

    // ���� ó�� �Լ�
    private void ProcessJudgement(GameObject note, int judgementIndex)
    {
        Debug.Log("���� ����");
        // ��Ʈ ����Ʈ���� ����
        if (upperLaneNotes.Contains(note))
        {
            upperLaneNotes.Remove(note);
        }
        else if (lowerLaneNotes.Contains(note))
        {
            lowerLaneNotes.Remove(note);
        }

        note.GetComponent<Note>().HideNote(); // ��Ʈ ����
        effectManager.JudgementEffect(judgementIndex); // ���� �̹��� ���
    }

    public void CheckTiming(string lane)
    {
        Debug.Log("�Լ� ����");

        //���� ����
        List<GameObject> noteList = lane == "upper" ? upperLaneNotes : lowerLaneNotes;

        //�ش� ���ο� ���� ��Ʈ�� �˻�
        for (int i = 0; i < availableNotes.Count; i++)
        {
            GameObject note = availableNotes[i];
            if (!noteList.Contains(note)) continue; // ���ο� �ش��ϴ� ��Ʈ�� �ƴϸ� �Ѿ

            Collider2D noteCollider = note.GetComponent<Collider2D>();

            // Perfect ����
            if (perfectCollider.IsTouching(noteCollider))
            {
                ProcessJudgement(note, 0); // Perfect ����
                availableNotes.RemoveAt(i); // ���� �Ϸ� �� ����
                return;
            }

            // Great ���� (Perfect�� �ش���� ���� ����)
            if (greatCollider.IsTouching(noteCollider))
            {
                ProcessJudgement(note, 1); // Great ����
                availableNotes.RemoveAt(i); // ���� �Ϸ� �� ����
                return;
            }
        }
    }
        
}
