using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneUiManager : MonoBehaviour
{
    public TalkManager talkManager;    // TalkManager reference
    public GameManager gameManager;    // GameManager reference
    public GameObject dialogueCanvas;  // UI canvas for dialogue
    public Text dialogueText;          // Text UI for dialogue
    public Text speakerNameText;       // UI for speaker's name
    public Image dialogueImage;        // Image UI for dialogue

    public bool isTalk;                // Whether the dialogue window is active
    public int talkIndex = 0;          // Dialogue index
    public string playerName = "player1";  // Player's name

    public GameObject sceneUiManagerObj; // For managing SceneUiManager display

    void Start()
    {
        // Locate TalkManager and GameManager on start
        talkManager = FindObjectOfType<TalkManager>();
        gameManager = FindObjectOfType<GameManager>();
        dialogueCanvas.SetActive(false); // Ensure the dialogue canvas is initially inactive
        sceneUiManagerObj.SetActive(false); // Initially set inactive
    }

    public void StartDialogueWithDelay(GameObject scanObj, int id)
    {
        if (isTalk) return;  // Prevents multiple dialogues from starting simultaneously

        sceneUiManagerObj.SetActive(true); // Show the UI manager object
        StartCoroutine(DialogueCoroutine(id)); // Start the dialogue coroutine
    }

    IEnumerator DialogueCoroutine(int id)
    {
        isTalk = true;  // Dialogue is now active

        yield return new WaitForSeconds(1f); // Wait for 1 second before showing dialogue

        while (true)
        {
            string speakerName;
            string talkData = talkManager.GetTalk(id, talkIndex, out speakerName);

            if (talkData == null)
            {
                isTalk = false;
                talkIndex = 0;
                dialogueCanvas.SetActive(false); // Hide the canvas when dialogue is done
                sceneUiManagerObj.SetActive(false); // Hide the UI manager object
                yield break;
            }

            // Set speaker name and dialogue text
            speakerNameText.text = (speakerName == "플레이어") ? playerName : speakerName;
            dialogueText.text = talkData;

            // Show dialogue canvas
            dialogueCanvas.SetActive(true);
            dialogueImage.color = new Color(1, 1, 1, 1);  // Set image visible or modify it as needed

            talkIndex++;

            yield return new WaitForSeconds(2f); // Dialogue visible for 2 seconds

            // Optionally hide dialogue canvas after showing
            dialogueCanvas.SetActive(false);
        }
    }
}
