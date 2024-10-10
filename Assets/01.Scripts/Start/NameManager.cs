using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    public InputField textbox;
    public Button confirmBtn;

    private string playerName = null;

    private void Start()
    {
        confirmBtn.interactable = false;
        confirmBtn.onClick.AddListener(OnConfirm);
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(textbox.text))
        {
            confirmBtn.interactable = true;
        }
    }

    private void OnConfirm()
    {
        playerName = textbox.text;
        Debug.Log(playerName);
    }
    //if textbox playerName!=null
    //

}
