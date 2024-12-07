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
    double currentTime = 0d; //���� �ð�
    double startDspTime = 0d; //��� �ð�
    float distanceToJudge;
    bool isPlay = false;

    AudioSource audioSource;
    public GameObject Target;
    TimingManager timingManager;

    //��Ʈ ���� ��ġ
    [SerializeField] Transform tfNoteAppearUpper = null;
    [SerializeField] Transform tfNoteAppearLower = null;
    //��Ʈ
    [SerializeField] GameObject goNoteUpper = null;
    [SerializeField] GameObject goNoteLower = null;

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
        public List<NoteData> notes; //��Ʈ �����͸� ������� �ҷ��� ����
    }

    private ChartData chartData;
    private int noteIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision) //���� �ۿ��� ��Ʈ ����
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
        audioSource = Target.GetComponent<AudioSource>();

        //�ܺ� JSON ���� �ҷ�����
        LoadChartFile("Assets/07.Charts/" + FileName + ".json");
        // ��Ʈ�� �̵��ؾ� �ϴ� �Ÿ�
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
                startDspTime = AudioSettings.dspTime + 0.05;  // +�� ���� �� dspTime ���� ��� ����
                audioSource.PlayScheduled(startDspTime);// AudioSource�� dspTime�� ���� ��� ����
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
