using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    /*
    public float NoteSpeed;
    public bool hasStarted;

    private void Start()
    {
        NoteSpeed = NoteSpeed / 60f;
    }

    private void Update()
    {
        if (!hasStarted)
        {
            if(Input.anyKeyDown)
            {
                hasStarted = true;
            }
        }
        else
        {
            transform.position += Vector3.right * NoteSpeed * Time.deltaTime;
        }
    }
    */

    
    public float noteSpeed = 400;

    Image noteImage;

    private void Start()
    {
        noteImage = GetComponent<Image>();
    }

    void Update()
    {
        transform.localPosition -= Vector3.right * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        //Destroy(this);
        noteImage.enabled = false;
        Debug.Log("제발제발제발");
    }
    
}
