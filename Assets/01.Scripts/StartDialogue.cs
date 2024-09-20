using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartDialogue : MonoBehaviour
{
    public GameObject textbox;
    public GameObject EndCursor;
    public Text ScriptText_dialogue;  // UI Text component for dialogue
    public string[] dialogue = { };   // Array for dialogue strings

    public int dialogue_count = 0;    // To track current dialogue index
    private PlayerController playerController;  // PlayerController reference

    public float WaitTime;

    void Start()
    {
        // PlayerController �ν��Ͻ��� ã��
        playerController = FindObjectOfType<PlayerController>();

        // ó������ �ؽ�Ʈ ��Ȱ��ȭ
        ScriptText_dialogue.gameObject.SetActive(false);
        textbox.gameObject.SetActive(false);
        EndCursor.gameObject.SetActive(false);

        // 1�� �� ��ȭ ����
        StartCoroutine(ShowDialogueWithDelay());
    }

    IEnumerator ShowDialogueWithDelay()
    {
        yield return new WaitForSeconds(WaitTime); // n�� ���

        // ù ��� ǥ�� �� �ؽ�Ʈ Ȱ��ȭ
        textbox.gameObject.SetActive(true);
        ScriptText_dialogue.gameObject.SetActive(true);

        EndCursor.gameObject.SetActive(true);

        // ��ȭ�� ���۵Ǹ� �÷��̾� �̵� ��Ȱ��ȭ
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
        // �����̽��ٷ� ��� �ѱ��
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
            ScriptText_dialogue.gameObject.SetActive(false);
            textbox.gameObject.SetActive(false);
            EndCursor.gameObject.SetActive(false);

            // ��ȭ�� ������ �÷��̾� �̵� �ٽ� Ȱ��ȭ
            if (playerController != null)
            {
                playerController.SetMove(true);
            }
        }
    }
}
