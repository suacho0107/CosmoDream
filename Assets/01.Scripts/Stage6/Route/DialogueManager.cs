using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Dialogue[] dialogues;
    public Text dialogueTextYunoh;
    public Text dialogueTextPlayer;
    public string dialogueFilePath; // JSON 파일 경로
    public string SceneName;
    public CanvasGroup BlackOut;


    int currentIndex = 0;

    private void Start()
    {
        LoadDialogue();
        ShowDialogue();
    }

    void LoadDialogue()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(dialogueFilePath);
        if (jsonFile!=null)
        {
            string json = jsonFile.text;
            dialogues = jsonHelper.FromJson<Dialogue>(json);
        }
        else
        {
            Debug.LogError("대화 파일을 찾을 수 없습니다");
        }
    }
    public void ShowDialogue()
    {
        if (currentIndex >= dialogues.Length)
        {
            Debug.Log("대화 끝");

            SceneManager.LoadScene(SceneName);
            return;
        }
        Dialogue currentDialogue = dialogues[currentIndex];

        if (currentDialogue.speaker == "PLAYER")
        {
            dialogueTextYunoh.text = "";
            dialogueTextPlayer.text = currentDialogue.message;
        }
        else if (currentDialogue.speaker == "YUNOH")
        {
            dialogueTextYunoh.text = currentDialogue.message;
            dialogueTextPlayer.text = "";
        }
        else if (currentDialogue.speaker == "CANCEL")
        {
            //화면 블랙아웃
            BlackOut.alpha = 1;
            dialogueTextYunoh.text = "";
            dialogueTextPlayer.text = currentDialogue.message;
        }

        currentIndex++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    public void NextDialogue()
    {
        ShowDialogue();
    }
}
