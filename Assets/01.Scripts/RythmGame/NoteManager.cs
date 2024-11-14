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

    //��Ʈ ���� ��ġ
    [SerializeField] Transform tfNoteAppearUpper = null;
    [SerializeField] Transform tfNoteAppearLower = null;

    //��Ʈ
    [SerializeField] GameObject goNoteUpper = null;
    [SerializeField] GameObject goNoteLower = null;

    private float noteSpeed; //��Ʈ �ӵ�

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
        noteSpeed = goNoteUpper.GetComponent<Note>().noteSpeed; //��� ��Ʈ �ӵ��� �����ϴٴ� ���� �Ͽ�

        //�ܺ� JSON ���� �ҷ�����(�����ʿ�)
        LoadChartFile("Assets/07.Charts/sampleChart.json");

        bpm = chartData.bpm;

        // ��Ʈ�� �̵��ؾ� �ϴ� �Ÿ�
        distanceToJudge = Mathf.Abs(tfNoteAppearUpper.localPosition.x - timingManager.timingRect[0].localPosition.x);
    }

    private void Update()
    {
        if (isStart)
        {
            if (!isPlay)
            {
                startDspTime = AudioSettings.dspTime + 0.05;  // 0.1�� ���� �� dspTime ���� ��� ����
                audioSource.PlayScheduled(startDspTime);      // AudioSource�� dspTime�� ���� ��� ����
                isPlay = true;
            }
            currentTime = AudioSettings.dspTime - startDspTime;

            if (noteIndex < chartData.notes.Count)
            {
                // ��Ʈ�� ���� ������ �����ؾ� �ϴ� �ð�
                float targetTime = chartData.notes[noteIndex].time;
                // �Ÿ��� noteSpeed�� �̵��ϴµ� �ɸ��� �ð�
                float travelTime = distanceToJudge / noteSpeed;
                Debug.Log(travelTime);

                // ��Ʈ�� travelTime ��ŭ ���� �����Ǿ�� ���� ������ ��Ȯ�� targetTime�� ������
                if (currentTime >= targetTime - travelTime)
                {
                    // ��Ʈ�� ������ ���� ����
                    Transform noteAppearPosition = chartData.notes[noteIndex].lane == "upper" ? tfNoteAppearUpper : tfNoteAppearLower;

                    // ��Ʈ ����
                    if (chartData.notes[noteIndex].lane == "upper")
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
