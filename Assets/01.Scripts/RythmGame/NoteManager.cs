using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class NoteManager : MonoBehaviour
{
    public bool isStart = false;
    public string FileName = "sampleChart1";
    public string nextScene;

    int bpm;
    double currentTime = 0d; //현재 시간
    double startDspTime = 0d; //재생 시간
    float distanceToJudge;
    bool isPlay = false;

    AudioSource audioSource;
    public GameObject Target;
    TimingManager timingManager;

    //노트 생성 위치
    [SerializeField] Transform tfNoteAppearUpper = null;
    [SerializeField] Transform tfNoteAppearLower = null;
    //노트
    [SerializeField] GameObject goNoteUpper = null;
    [SerializeField] GameObject goNoteLower = null;

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
        audioSource = Target.GetComponent<AudioSource>();

        //외부 JSON 파일 불러오기
        LoadChartFile("Assets/07.Charts/" + FileName + ".json");
        // 노트가 이동해야 하는 거리
        distanceToJudge = Mathf.Abs(tfNoteAppearUpper.localPosition.x - timingManager.timingRect[0].localPosition.x);
        Debug.Log(distanceToJudge);
        bpm = chartData.bpm;
    }

    private void FixedUpdate()
    {
        if (isStart)
        {
            if (!isPlay)
            {
                startDspTime = AudioSettings.dspTime + 0.05;  // +초 지연 후 dspTime 기준 재생 시작
                audioSource.PlayScheduled(startDspTime);// AudioSource를 dspTime에 맞춰 재생 예약
                StartCoroutine(EndPlay());
                isPlay = !isPlay;
            }

            currentTime = AudioSettings.dspTime - startDspTime;

            if (noteIndex < chartData.notes.Count)
            {
                float targetTime = chartData.notes[noteIndex].time;
                float timeToPerfect = distanceToJudge / (1100f * (bpm / 60f));

                if (currentTime >= targetTime - timeToPerfect)
                {
                    Transform noteAppearPosition = chartData.notes[noteIndex].lane == "upper" ? tfNoteAppearUpper : tfNoteAppearLower;
                    GameObject t_note = chartData.notes[noteIndex].lane == "upper" ? goNoteUpper : goNoteLower;

                    GameObject newNote = Instantiate(t_note, noteAppearPosition.position, Quaternion.identity);
                    //newNote.GetComponent<Note>().Initialize(noteSpeedMultiplier, distanceToJudge / timeToPerfect);

                    newNote.transform.SetParent(this.transform);

                    if (chartData.notes[noteIndex].lane == "upper")
                        timingManager.upperLaneNotes.Add(newNote);
                    else
                        timingManager.lowerLaneNotes.Add(newNote);

                    noteIndex++;
                }
            }

        }

        
    }

    private void LoadChartFile(string path)
    {
        string json = File.ReadAllText(path);
        chartData = JsonUtility.FromJson<ChartData>(json);
    }

    private IEnumerator EndPlay()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        yield return new WaitForSeconds(5f);

        Debug.Log(nextScene);
        SceneManager.LoadScene(nextScene);
    }
}
