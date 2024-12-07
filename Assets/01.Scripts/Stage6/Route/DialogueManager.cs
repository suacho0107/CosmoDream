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
    public string dialogueFilePath; // JSON ���� ���
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
        string fullPath = "Assets/01.Scripts/Stage6/Route/Script/" + dialogueFilePath +".json";
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            dialogues = jsonHelper.FromJson<Dialogue>(json);
        }
        else
        {
            Debug.LogError("��ȭ ������ ã�� �� �����ϴ�: " + fullPath);
        }
    }
    public void ShowDialogue()
    {
        if (currentIndex >= dialogues.Length)
        {
            Debug.Log("��ȭ ��");

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
            //ȭ�� ���ƿ�
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
