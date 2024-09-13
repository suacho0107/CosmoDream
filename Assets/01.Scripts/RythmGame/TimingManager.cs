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

    // 판정 가능한 노트를 저장할 리스트 (충돌 후 대기 중인 노트)
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
            availableNotes.Add(collision.gameObject);  // 노트 추가 (판정 대기 중)
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note") && collision == missCollider)
        {
            GameObject note = collision.gameObject;
            ProcessJudgement(note, 2); // Miss 판정
        }
    }

    // 판정 처리 함수
    private void ProcessJudgement(GameObject note, int judgementIndex)
    {
        Debug.Log("판정 성공");
        // 노트 리스트에서 제거
        if (upperLaneNotes.Contains(note))
        {
            upperLaneNotes.Remove(note);
        }
        else if (lowerLaneNotes.Contains(note))
        {
            lowerLaneNotes.Remove(note);
        }

        note.GetComponent<Note>().HideNote(); // 노트 숨김
        effectManager.JudgementEffect(judgementIndex); // 판정 이미지 출력
    }

    public void CheckTiming(string lane)
    {
        Debug.Log("함수 실행");

        //라인 선택
        List<GameObject> noteList = lane == "upper" ? upperLaneNotes : lowerLaneNotes;

        //해당 라인에 속한 노트만 검사
        for (int i = 0; i < availableNotes.Count; i++)
        {
            GameObject note = availableNotes[i];
            if (!noteList.Contains(note)) continue; // 라인에 해당하는 노트가 아니면 넘어감

            Collider2D noteCollider = note.GetComponent<Collider2D>();

            // Perfect 판정
            if (perfectCollider.IsTouching(noteCollider))
            {
                ProcessJudgement(note, 0); // Perfect 판정
                availableNotes.RemoveAt(i); // 판정 완료 후 제거
                return;
            }

            // Great 판정 (Perfect에 해당되지 않을 때만)
            if (greatCollider.IsTouching(noteCollider))
            {
                ProcessJudgement(note, 1); // Great 판정
                availableNotes.RemoveAt(i); // 판정 완료 후 제거
                return;
            }
        }
    }
        
}
