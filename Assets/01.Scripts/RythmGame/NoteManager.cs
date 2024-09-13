using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    private double currentTime = 0d;

    [SerializeField] Transform tfNoteAppearUpper = null;
    [SerializeField] Transform tfNoteAppearLower = null;
    [SerializeField] GameObject goNoteUpper = null;
    [SerializeField] GameObject goNoteLower = null;

    private TimingManager timingManager;

    //JSON���� �о���� �����͸� ���� Ŭ����
    [System.Serializable]
    public class NoteData
    {
        public float time;
        public string lane;
    }

    [System.Serializable]
    public class ChartData
    {
        public int bpm;
        public List<NoteData> notes;
    }

    private ChartData chartData;
    private int noteIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("upperNote"))
        {
            timingManager.upperLaneNotes.Remove(collision.gameObject);  //��Ʈ ����Ʈ�� ��Ʈ ����
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("lowerNote"))
        {
            timingManager.lowerLaneNotes.Remove(collision.gameObject);  //��Ʈ ����Ʈ�� ��Ʈ ����
            Destroy(collision.gameObject);
        }
    }

    private void Start()
    {
        timingManager = FindObjectOfType<TimingManager>();

        //�ܺ� JSON ���� �ҷ�����(�����ʿ�)
        LoadChartFile("Assets/07.Charts/sampleChart.json");

        bpm = chartData.bpm;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        //��Ʈ ���� ����
        if(noteIndex < chartData.notes.Count && currentTime >= chartData.notes[noteIndex].time)
        {
            //��Ʈ�� ������ ���� ����
            Transform noteAppearPosition = chartData.notes[noteIndex].lane == "upper" ? tfNoteAppearUpper : tfNoteAppearLower;

            //��Ʈ ����
            if(chartData.notes[noteIndex].lane == "upper")
            {
                GameObject t_note = Instantiate(goNoteUpper, noteAppearPosition.position, Quaternion.identity);
                t_note.transform.SetParent(this.transform);
                timingManager.upperLaneNotes.Add(t_note); //��Ʈ ����Ʈ�� ��Ʈ �߰�
            }
            else
            {
                GameObject t_note = Instantiate(goNoteLower, noteAppearPosition.position, Quaternion.identity);
                t_note.transform.SetParent(this.transform);
                timingManager.lowerLaneNotes.Add(t_note); //��Ʈ ����Ʈ�� ��Ʈ �߰�
            }

            noteIndex++;
        }
    }

    private void LoadChartFile(string path)
    {
        string json = File.ReadAllText(path);
        chartData = JsonUtility.FromJson<ChartData>(json);
    }
}
