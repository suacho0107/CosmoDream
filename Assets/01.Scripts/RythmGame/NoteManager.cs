using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public enum LaneType { Upper, Lower }

public class NoteManager : MonoBehaviour
{
    [SerializeField] private string FileName = "sampleChart1";
    [SerializeField] private string nextScene;
    [SerializeField] private Transform tfNoteAppearUpper;
    [SerializeField] private Transform tfNoteAppearLower;
    [SerializeField] private GameObject goNoteUpper;
    [SerializeField] private GameObject goNoteLower;
    [SerializeField] private TimingManager timingManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject Target;

    public bool isStart = false;

    private double currentTime = 0d;
    private double startDspTime = 0d;
    private bool isPlay = false;
    private int noteIndex = 0;

    private float distanceToJudge;
    private int bpm;

    public float offset = 0.05f;

    [System.Serializable]
    public class NoteData
    {
        public float time;
        public string lane; // "upper" or "lower"
        public LaneType LaneType
        {
            get { return lane.ToLower() == "upper" ? LaneType.Upper : LaneType.Lower; }
        }
    }

    [System.Serializable]
    public class ChartData
    {
        public int bpm;
        public List<NoteData> notes;
    }

    private ChartData chartData;

    private void Start()
    {
        // Inspector에서 timingManager, audioSource 할당되어 있다고 가정
        LoadChartFile(Path.Combine(Application.dataPath, "07.Charts", FileName + ".json"));

        distanceToJudge = Mathf.Abs(tfNoteAppearUpper.localPosition.x - timingManager.GetComponent<RectTransform>().localPosition.x);
        bpm = chartData.bpm;
    }

    private void Update()
    {
        if (!isStart) return;

        if (!isPlay)
        {
            startDspTime = AudioSettings.dspTime + offset;
            audioSource.PlayScheduled(startDspTime);
            StartCoroutine(EndPlay());
            isPlay = true;
        }

        currentTime = AudioSettings.dspTime - startDspTime;
        SpawnNotes();
    }

    private void SpawnNotes()
    {
        if (noteIndex >= chartData.notes.Count) return;

        float noteSpeed = 1200f;
        float timeToPerfect = distanceToJudge / noteSpeed;

        // 정렬된 데이터를 기반으로 스폰
        while (noteIndex < chartData.notes.Count)
        {
            double targetTime = chartData.notes[noteIndex].time;
            if (currentTime < targetTime - timeToPerfect) break;

            LaneType laneType = chartData.notes[noteIndex].LaneType;
            Transform spawnPos = (laneType == LaneType.Upper) ? tfNoteAppearUpper : tfNoteAppearLower;
            GameObject notePrefab = (laneType == LaneType.Upper) ? goNoteUpper : goNoteLower;

            GameObject newNoteObj = Instantiate(notePrefab, spawnPos.position, Quaternion.identity, this.transform);
            Note newNote = newNoteObj.GetComponent<Note>();

            timingManager.AddNote(newNote, laneType);
            noteIndex++;
        }
    }

    private void LoadChartFile(string path)
    {
        string json = File.ReadAllText(path);
        chartData = JsonUtility.FromJson<ChartData>(json);
        // 정렬 필요시
        chartData.notes.Sort((a, b) => a.time.CompareTo(b.time));
    }

    private IEnumerator EndPlay()
    {
        yield return new WaitForSeconds((float)audioSource.clip.length + 0.5f);
        PuzzleClear puzzleclear = FindObjectOfType<PuzzleClear>();
        if(puzzleclear != null)
        {
            puzzleclear.CompletePuzzle();
        }
        FadeManager.instance.ChangeScene(nextScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
