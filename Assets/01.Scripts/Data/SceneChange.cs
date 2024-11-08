using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int SceneIndex; // 전환할 씬 인덱스
    public bool isEdge;

    public void ChangeScene()
    {
        FadeManager.instance.ChangeScene(SceneIndex);
    }

    public void ChangeScene(string sceneName)
    {
        FadeManager.instance.ChangeScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isEdge)
        FadeManager.instance.ChangeScene(SceneIndex);
    }
}