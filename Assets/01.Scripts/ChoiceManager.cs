using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ChoiceManager : MonoBehaviour
{
    public GameObject choicePanel;
    public GameObject endCursor;
    public GameObject text1;
    public GameObject text2;

    public int puzzleScene;
    public bool is2Cliked = false;

    TalkManager talkManager;

    void Start()
    {
        choicePanel.SetActive(false);
        talkManager = FindObjectOfType<TalkManager>();

        AddEventTrigger(text1, OnText1Click);
        AddEventTrigger(text2, OnText2Click);

        AddPointerEvents(text1);
        AddPointerEvents(text2);

        // 엔드 커서 초기 위치 설정
        endCursor.transform.position = new Vector3(endCursor.transform.position.x, text1.transform.position.y, text1.transform.position.z);
    }

    void AddEventTrigger(GameObject textObject, UnityEngine.Events.UnityAction callback)
    {
        EventTrigger trigger = textObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        entry.callback.AddListener((data) => { callback.Invoke(); });
        trigger.triggers.Add(entry);
    }

    void AddPointerEvents(GameObject textObject)
    {
        EventTrigger trigger = textObject.AddComponent<EventTrigger>();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        enterEntry.callback.AddListener((data) => { UpdateCursorPosition(textObject); });
        trigger.triggers.Add(enterEntry);
    }

    public void ShowChoices()
    {
        choicePanel.SetActive(true);
        is2Cliked = false;
    }

    void UpdateCursorPosition(GameObject textObject)
    {
        Vector3 cursorPos = endCursor.transform.position;

        if (textObject == text1)
        {
            cursorPos.y = textObject.transform.position.y;
        }

        else if (textObject == text2)
        {
            cursorPos.y = textObject.transform.position.y;
        }

        endCursor.transform.position = cursorPos;
    }

    private void OnText1Click()
    {
        SceneManager.LoadScene(puzzleScene);
    }

    private void OnText2Click()
    {
        is2Cliked = true;
        choicePanel.SetActive(false);
        talkManager.EndTalk(0);
    }
}
