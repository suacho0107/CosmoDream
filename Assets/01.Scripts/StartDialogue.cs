using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartDialogue : MonoBehaviour
{
    public GameObject textbox;
    public GameObject EndCursor;
    public Text ScriptText_dialogue;  // UI Text component for dialogue
    public Text DialogueName;
    public string Name;
    public string[] dialogue = { };   // Array for dialogue strings
    string sceneName;

    public int dialogue_count = 0;    // To track current dialogue index
    private PlayerController playerController;  // PlayerController reference

    public float WaitTime;

    void Start()
    {
        // PlayerController ?????????? ????
        playerController = FindObjectOfType<PlayerController>();
        sceneName = SceneManager.GetActiveScene().name;
        if (GameManager.instance != null && GameManager.instance.HasDialogueRun(sceneName))
        {
            Destroy(gameObject);
            return;
        }

        SwitchPanel(false);
        DialogueName.text = Name;

        // 1?? ?? ???? ????
        StartCoroutine(ShowDialogueWithDelay());
        GameManager.instance.SetDialogueRun(sceneName);
        GameManager.instance.isTalk = true;
        Debug.Log(GameManager.instance.isTalk);
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
        if (textbox.activeSelf && Input.GetKeyDown(KeyCode.Space))
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
            GameManager.instance.isTalk = false;
            Debug.Log(GameManager.instance.isTalk);

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
