using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    private double currentTime = 0d;
    private double startDspTime = 0d;
    public bool isStart = false;
    private float distanceToJudge;
    bool isPlay = false;

    public AudioSource audioSource;

    //노트 생성 위치
    [SerializeField] Transform tfNoteAppearUpper = null;
    [SerializeField] Transform tfNoteAppearLower = null;

    //노트
    [SerializeField] GameObject goNoteUpper = null;
    [SerializeField] GameObject goNoteLower = null;

    private float noteSpeed; //노트 속도

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
        public List<NoteData> notes; //노트 데이터를 순서대로 불러와 저장
    }

    private ChartData chartData;
    private int noteIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision) //영역 밖에서 노트 제거
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
        noteSpeed = goNoteUpper.GetComponent<Note>().noteSpeed; //모든 노트 속도는 동일하다는 가정 하에

        //외부 JSON 파일 불러오기(수정필요)
        LoadChartFile("Assets/07.Charts/sampleChart.json");

        bpm = chartData.bpm;

        // 노트가 이동해야 하는 거리
        distanceToJudge = Mathf.Abs(tfNoteAppearUpper.localPosition.x - timingManager.timingRect[0].localPosition.x);
    }

    private void Update()
    {
        if (isStart)
        {
            if (!isPlay)
            {
                startDspTime = AudioSettings.dspTime + 0.05;  // 0.1초 지연 후 dspTime 기준 재생 시작
                audioSource.PlayScheduled(startDspTime);      // AudioSource를 dspTime에 맞춰 재생 예약
                isPlay = true;
            }
            currentTime = AudioSettings.dspTime - startDspTime;

            if (noteIndex < chartData.notes.Count)
            {
                // 노트가 판정 구역에 도착해야 하는 시간
                float targetTime = chartData.notes[noteIndex].time;
                // 거리를 noteSpeed로 이동하는데 걸리는 시간
                float travelTime = distanceToJudge / noteSpeed;
                Debug.Log(travelTime);

                // 노트가 travelTime 만큼 일찍 생성되어야 판정 구역에 정확히 targetTime에 도착함
                if (currentTime >= targetTime - travelTime)
                {
                    // 노트가 생성될 라인 결정
                    Transform noteAppearPosition = chartData.notes[noteIndex].lane == "upper" ? tfNoteAppearUpper : tfNoteAppearLower;

                    // 노트 생성
                    if (chartData.notes[noteIndex].lane == "upper")
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
            //if (noteIndex < chartData.notes.Count)
            //{
            //    float targetTime = chartData.notes[noteIndex].time;
            //    float travelTime = distanceToJudge / noteSpeed;

            //    if (currentTime >= targetTime - travelTime)
            //    {
            //        Transform noteAppearPosition = chartData.notes[noteIndex].lane == "upper" ? tfNoteAppearUpper : tfNoteAppearLower;

            //        if (chartData.notes[noteIndex].lane == "upper")
            //        {
            //            GameObject t_note = Instantiate(goNoteUpper, noteAppearPosition.position, Quaternion.identity);
            //            t_note.transform.SetParent(this.transform);
            //            timingManager.upperLaneNotes.Add(t_note);
            //        }
            //        else
            //        {
            //            GameObject t_note = Instantiate(goNoteLower, noteAppearPosition.position, Quaternion.identity);
            //            t_note.transform.SetParent(this.transform);
            //            timingManager.lowerLaneNotes.Add(t_note);
            //        }

            //        noteIndex++;
            //    }
            //}
        }

        
    }

    private void LoadChartFile(string path)
    {
        string json = File.ReadAllText(path);
        chartData = JsonUtility.FromJson<ChartData>(json);
    }
}
