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
    public string dialogueFilePath; // JSON 파일 경로
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
            Debug.LogError("대화 파일을 찾을 수 없습니다");
        }
    }
    public void ShowDialogue()
    {
        if (currentIndex >= dialogues.Length)
        {
            Debug.Log("대화 끝");
            //페이드
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
            dialogueText.text = "(퍼즐을 풀까?)";
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
        //페이드
        SceneManager.LoadScene(SceneName);
    }

    public void NextDialogue()
    {
        ShowDialogue();
    }
}
