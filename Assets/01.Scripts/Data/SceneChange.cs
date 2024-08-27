using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int SceneIndex; // 전환할 씬 인덱스

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}