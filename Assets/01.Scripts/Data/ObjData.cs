using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public int id;
    public bool isNpc;
    public ObjectType objectType;
    public GameObject Display;

    void Start() {
        if (Display == null)
        return;
        else
        Display.SetActive(false);
    }

    public enum ObjectType
    {
        Talkable,
        NpcBubble,
        SceneChange,
        ImageDisplay
    }
}
