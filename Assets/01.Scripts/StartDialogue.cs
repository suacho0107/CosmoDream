using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartDialogue : MonoBehaviour
{
    public GameObject textbox;
    public GameObject EndCursor;
    public Text ScriptText_dialogue;  // UI Text component for dialogue
    public Text DialogueName;
    public string Name;
    public string[] dialogue = { };   // Array for dialogue strings

    public int dialogue_count = 0;    // To track current dialogue index
    private PlayerController playerController;  // PlayerController reference

    public float WaitTime;

    void Start()
    {
        // PlayerController ?????????? ????
        playerController = FindObjectOfType<PlayerController>();

        if (GameManager.instance != null && GameManager.instance.startDialogHasRun)
        {
            SwitchPanel(false);
            return;
        }

        // ???????? ?????? ????????
        GameManager.instance.startDialogHasRun = true;
        SwitchPanel(false);
        DialogueName.text = Name;

        // 1?? ?? ???? ????
        StartCoroutine(ShowDialogueWithDelay());
    }

    IEnumerator ShowDialogueWithDelay()
    {
        yield return new WaitForSeconds(WaitTime); // n?? ????

        // ?? ???? ???? ?? ?????? ??????
        SwitchPanel(true);

        // ?????? ???????? ???????? ???? ????????
        if (playerController != null)
        {
            playerController.SetMove(false);
        }

        if (dialogue.Length > 0)
        {
            ScriptText_dialogue.text = dialogue[dialogue_count];
        }
    }

    void Update()
    {
        // ???????????? ???? ??????
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        // Check if more dialogues are available
        dialogue_count++;
        if (dialogue_count < dialogue.Length)
        {
            // Display the next dialogue
            ScriptText_dialogue.text = dialogue[dialogue_count];
        }
        else
        {
            // If no more dialogues, deactivate the dialogue text
            SwitchPanel(false);

            // ?????? ?????? ???????? ???? ???? ??????
            if (playerController != null)
            {
                playerController.SetMove(true);
            }
        }
    }

    public void SwitchPanel(bool tf) // true false
    {
        ScriptText_dialogue.gameObject.SetActive(tf);
        textbox.gameObject.SetActive(tf);
        EndCursor.gameObject.SetActive(tf);
    }
}
