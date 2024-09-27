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
        //타이밍 박스 perfect->miss
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
        Debug.Log("함수 실행");
        // 해당 라인의 노트 리스트 선택 (윗줄, 아랫줄)
        List<GameObject> noteList = lane == "upper" ? upperLaneNotes : lowerLaneNotes;

        for (int i = 0; i < noteList.Count; i++)
        {
            float t_notePosX = noteList[i].transform.localPosition.x;
            
            // 판정은 Perfect -> Great -> Miss 순서로 이루어짐 (작은 영역부터 검사)
            for (int x = 0; x < timingBoxs.Length - 1; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    noteList[i].GetComponent<Note>().HideNote(); // 노트 숨김
                    noteList.RemoveAt(i); // 리스트에서 제거
                    effectManager.JudgementEffect(x, lane); // x에 해당하는 판정 출력 (0: Perfect, 1: Great, 2: Miss)

                    Debug.Log("실행됨");
                    return;
                }
            }
        }

        // 해당 범위에 없는 경우 Miss 처리
        //effectManager.JudgementEffect(timingBoxs.Length);
    }

    public void CheckMiss(string lane)
    {
        List<GameObject> noteList = lane == "upper" ? upperLaneNotes : lowerLaneNotes;

        for (int i = 0; i < noteList.Count; i++)
        {
            float t_notePosX = noteList[i].transform.localPosition.x;

            // Miss 영역에 해당하면 즉시 Miss 처리
            if (timingBoxs[2].x <= t_notePosX && t_notePosX <= timingBoxs[2].y) // Miss 영역에 해당
            {
                noteList.RemoveAt(i); // 리스트에서 제거
                effectManager.JudgementEffect(2, lane); // Miss 판정 출력
                Debug.Log("Miss 처리됨");
                return;
            }
        }
    }
}
