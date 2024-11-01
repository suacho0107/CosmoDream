using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // 씬 관리 기능을 위해 추가

public class DialogueBox : MonoBehaviour
{
    public GameObject textbox;
    public GameObject EndCursor;
    public Text ScriptText_dialogue;  // UI Text component for dialogue
    public Text DialogueName;
    public string Name;
    public string[] dialogue = { };   // Array for dialogue strings
    public GameObject fam;            // 첫 번째 이미지
    public GameObject headset;        // 두 번째 이미지

    public int dialogue_count = 0;    // To track current dialogue index
    private PlayerController playerController;  // PlayerController reference
    public float WaitTime;
    public string NextSceneName;      // 이동할 씬 이름

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        ScriptText_dialogue.gameObject.SetActive(false);
        textbox.gameObject.SetActive(false);
        EndCursor.gameObject.SetActive(false);
        DialogueName.text = Name;

        fam.SetActive(true);            // 초기 이미지 설정
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
        // 다음 대화로 진행
        dialogue_count++;
        if (dialogue_count < dialogue.Length)
        {
            ScriptText_dialogue.text = dialogue[dialogue_count];

            // 특정 대화 인덱스에서 이미지 변경
            if (dialogue_count == 4)  // 예시로 4번째 대화에서 이미지를 변경
            {
                fam.SetActive(false);
                headset.SetActive(true);
            }

        }
        else
        {
            // 대화가 끝나면 비활성화
            ScriptText_dialogue.gameObject.SetActive(false);
            textbox.gameObject.SetActive(false);
            EndCursor.gameObject.SetActive(false);

            if (playerController != null)
            {
                playerController.SetMove(true);
            }

            // 대화가 끝나면 씬 이동
            SceneManager.LoadScene("1-1 train");
        }
    }
}
