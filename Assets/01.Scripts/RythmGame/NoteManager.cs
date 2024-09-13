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

    //JSON에서 읽어들일 데이터를 담을 클래스
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
            timingManager.upperLaneNotes.Remove(collision.gameObject);  //노트 리스트에 노트 제거
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("lowerNote"))
        {
            timingManager.lowerLaneNotes.Remove(collision.gameObject);  //노트 리스트에 노트 제거
            Destroy(collision.gameObject);
        }
    }

    private void Start()
    {
        timingManager = FindObjectOfType<TimingManager>();

        //외부 JSON 파일 불러오기(수정필요)
        LoadChartFile("Assets/07.Charts/sampleChart.json");

        bpm = chartData.bpm;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        //노트 출현 관련
        if(noteIndex < chartData.notes.Count && currentTime >= chartData.notes[noteIndex].time)
        {
            //노트가 생성될 라인 결정
            Transform noteAppearPosition = chartData.notes[noteIndex].lane == "upper" ? tfNoteAppearUpper : tfNoteAppearLower;

            //노트 생성
            if(chartData.notes[noteIndex].lane == "upper")
            {
                GameObject t_note = Instantiate(goNoteUpper, noteAppearPosition.position, Quaternion.identity);
                t_note.transform.SetParent(this.transform);
                timingManager.upperLaneNotes.Add(t_note); //노트 리스트에 노트 추가
            }
            else
            {
                GameObject t_note = Instantiate(goNoteLower, noteAppearPosition.position, Quaternion.identity);
                t_note.transform.SetParent(this.transform);
                timingManager.lowerLaneNotes.Add(t_note); //노트 리스트에 노트 추가
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
