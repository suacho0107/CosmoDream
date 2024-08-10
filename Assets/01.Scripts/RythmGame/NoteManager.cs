using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    private double currentTime = 0d;

    [SerializeField] Transform tfNoteAppear = null;
    [SerializeField] GameObject goNote = null;

    private TimingManager timingManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            timingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void Start()
    {
        timingManager = GetComponent<TimingManager>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            timingManager.boxNoteList.Add(t_note);
            currentTime -= 60d / bpm;
        }
    }
}
