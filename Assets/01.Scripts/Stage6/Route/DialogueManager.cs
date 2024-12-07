using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public Dialogue[] dialogues;
    public Text dialogueTextYunoh;
    public Text dialogueTextPlayer;
    public string dialogueFilePath; // JSON ���� ���

    int currentIndex = 0;

    private void Start()
    {
        LoadDialogue();
        ShowDialogue();
    }

    void LoadDialogue()
    {
        string fullPath = "Assets/01.Scripts/Stage6/Route/" + dialogueFilePath +".json";
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
        else if (currentDialogue.speaker == "Cancel")
        {

        }

        currentIndex++;
    }

    public void NextDialogue()
    {
        ShowDialogue();
    }
}
