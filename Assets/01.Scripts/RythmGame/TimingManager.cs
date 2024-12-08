using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    [SerializeField] private RectTransform[] timingRect = null;
    [SerializeField] private EffectManager effectManager;

    private Vector2[] timingBoxes;
    private Queue<Note> upperLaneNotes = new Queue<Note>();
    private Queue<Note> lowerLaneNotes = new Queue<Note>();

    private void Start()
    {
        timingBoxes = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            float centerX = timingRect[i].localPosition.x;
            float halfW = timingRect[i].rect.width * 0.5f;
            timingBoxes[i] = new Vector2(centerX - halfW, centerX + halfW);
        }
    }

    private void Update()
    {
        CheckMiss(LaneType.Upper);
        CheckMiss(LaneType.Lower);
    }

    public void CheckTiming(LaneType lane)
    {
        Queue<Note> noteQueue = GetNoteQueue(lane);
        if (noteQueue.Count == 0) return;

        Note firstNote = noteQueue.Peek();
        if (firstNote == null) return;

        float posX = firstNote.transform.localPosition.x;

        // Perfect -> Great 판정
        for (int i = 0; i < timingBoxes.Length - 1; i++)
        {
            if (posX >= timingBoxes[i].x && posX <= timingBoxes[i].y)
            {
                firstNote.HideNote();
                noteQueue.Dequeue();
                effectManager.JudgementEffect(i, lane);
                return;
            }
        }
    }

    public void CheckMiss(LaneType lane)
    {
        Queue<Note> noteQueue = GetNoteQueue(lane);
        if (noteQueue.Count == 0) return;

        // Miss 영역을 마지막 인덱스로 가정
        Vector2 missBox = timingBoxes[timingBoxes.Length - 1];
        Note firstNote = noteQueue.Peek();
        if (firstNote == null) return;

        float posX = firstNote.transform.localPosition.x;

        if (posX >= missBox.x && posX <= missBox.y)
        {
            noteQueue.Dequeue();
            effectManager.JudgementEffect(2, lane);
        }
    }

    private Queue<Note> GetNoteQueue(LaneType lane)
    {
        return (lane == LaneType.Upper) ? upperLaneNotes : lowerLaneNotes;
    }

    public void AddNote(Note note, LaneType lane)
    {
        if (lane == LaneType.Upper) upperLaneNotes.Enqueue(note);
        else lowerLaneNotes.Enqueue(note);
    }
}
