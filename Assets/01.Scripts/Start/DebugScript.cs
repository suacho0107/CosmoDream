using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("00.Scenes/Stage2/2");
    }
}
