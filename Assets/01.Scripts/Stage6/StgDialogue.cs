using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class StgDialogue : MonoBehaviour
{

    public Dialogue[] dialogues;
    public Text nameText;
    public Text dialogueText;
    public string dialogueFilePath; // JSON ���� ���
    public string SceneName;
    public GameObject Player;
    public GameObject Yunoh;
    public GameObject Btn;


    int currentIndex = 0;
    DataController datacontroller;

    private void Start()
    {
        datacontroller = FindObjectOfType<DataController>();
        LoadDialogue();
        ShowDialogue();
    }

    void LoadDialogue()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(dialogueFilePath);
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            dialogues = jsonHelper.FromJson<Dialogue>(json);
        }
        else
        {
            Debug.LogError("��ȭ ������ ã�� �� �����ϴ�");
        }
    }
    public void ShowDialogue()
    {
        if (currentIndex >= dialogues.Length)
        {
            Debug.Log("��ȭ ��");
            //���̵�
            //SceneManager.LoadScene(SceneName);
            return;
        }
        Dialogue currentDialogue = dialogues[currentIndex];

        if (currentDialogue.speaker == "PLAYER")
        {
            Player.SetActive(true);
            Yunoh.SetActive(false);
            nameText.text = datacontroller.gameData.name;
            dialogueText.text = currentDialogue.message;
        }
        else if (currentDialogue.speaker == "YUNOH")
        {
            Player.SetActive(false);
            Yunoh.SetActive(true);
            nameText.text = "YUNOH";
            dialogueText.text = currentDialogue.message;
        }
        else if (currentDialogue.speaker == "CHOICE")
        {
            Player.SetActive(true);
            Yunoh.SetActive(false);
            nameText.text = "";
            dialogueText.text = "(������ Ǯ��?)";
            Btn.SetActive(true);
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

    public void NextBtn()
    {
        //���̵�
        SceneManager.LoadScene(SceneName);
    }

    public void NextDialogue()
    {
        ShowDialogue();
    }
}
