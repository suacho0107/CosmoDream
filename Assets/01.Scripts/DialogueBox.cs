using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �� ���� ����� ���� �߰�

public class DialogueBox : MonoBehaviour
{
    public GameObject textbox;
    public GameObject EndCursor;
    public Text ScriptText_dialogue;  // UI Text component for dialogue
    public Text DialogueName;
    public string Name;
    public string[] dialogue = { };   // Array for dialogue strings
    public GameObject fam;            // ù ��° �̹���
    public GameObject headset;        // �� ��° �̹���

    public int dialogue_count = 0;    // To track current dialogue index
    private PlayerController playerController;  // PlayerController reference
    public float WaitTime;
    public string NextSceneName;      // �̵��� �� �̸�

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        ScriptText_dialogue.gameObject.SetActive(false);
        textbox.gameObject.SetActive(false);
        EndCursor.gameObject.SetActive(false);
        DialogueName.text = Name;

        fam.SetActive(true);            // �ʱ� �̹��� ����
        headset.SetActive(false);

        StartCoroutine(ShowDialogueWithDelay());
    }

    IEnumerator ShowDialogueWithDelay()
    {
        yield return new WaitForSeconds(WaitTime);

        textbox.gameObject.SetActive(true);
        ScriptText_dialogue.gameObject.SetActive(true);
        EndCursor.gameObject.SetActive(true);

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        // ���� ��ȭ�� ����
        dialogue_count++;
        if (dialogue_count < dialogue.Length)
        {
            ScriptText_dialogue.text = dialogue[dialogue_count];

            // Ư�� ��ȭ �ε������� �̹��� ����
            if (dialogue_count == 4)  // ���÷� 4��° ��ȭ���� �̹����� ����
            {
                fam.SetActive(false);
                headset.SetActive(true);
            }

        }
        else
        {
            // ��ȭ�� ������ ��Ȱ��ȭ
            ScriptText_dialogue.gameObject.SetActive(false);
            textbox.gameObject.SetActive(false);
            EndCursor.gameObject.SetActive(false);

            if (playerController != null)
            {
                playerController.SetMove(true);
            }

            // ��ȭ�� ������ �� �̵�
            SceneManager.LoadScene("1-1 train");
        }
    }
}
