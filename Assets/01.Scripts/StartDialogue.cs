using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartDialogue : MonoBehaviour
{
    public GameObject textbox;
    public GameObject EndCursor;
    public Text ScriptText_dialogue;  // UI Text component for dialogue
    public string[] dialogue = { };    // Array for dialogue strings

    public int dialogue_count = 0;    // To track current dialogue index

    void Start()
    {
        // 처음에는 텍스트 비활성화
        ScriptText_dialogue.gameObject.SetActive(false);
        textbox.gameObject.SetActive(false);
        EndCursor.gameObject.SetActive(false);
        // 1초 후 대화 시작
        StartCoroutine(ShowDialogueWithDelay());
    }

    IEnumerator ShowDialogueWithDelay()
    {
        yield return new WaitForSeconds(2f); // 1초 대기
        // 첫 대사 표시 및 텍스트 활성화
        textbox.gameObject.SetActive(true);
        ScriptText_dialogue.gameObject.SetActive(true);

        // EndCursor 위치 설정 (예: 텍스트박스 오른쪽 하단)
        // RectTransform endCursorRect = EndCursor.GetComponent<RectTransform>();
       // endCursorRect.anchoredPosition = new Vector2(300, -50);  // 적절한 위치로 조정

        EndCursor.gameObject.SetActive(true);

        if (dialogue.Length > 0)
        {
            ScriptText_dialogue.text = dialogue[dialogue_count];
        }
    }

    void Update()
    {
        // 스페이스바로 대사 넘기기
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
        }
    }
}
