using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode KeyToPress;
    public GameObject PerEffect;
    public GameObject MissEffect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                if(Mathf.Abs(transform.position.x) > 0) //임의로 수정
                {
                    Debug.Log("Perfect");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            if (gameObject.activeSelf == true)
            {
                //GameManager.instance.NoteMiss();
                canBePressed = false;
                Instantiate(MissEffect, transform.position, MissEffect.transform.rotation);
            }
        }
    }
}
