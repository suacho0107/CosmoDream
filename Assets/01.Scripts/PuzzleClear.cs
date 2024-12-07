using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleClear : MonoBehaviour
{
    public int stageNumber;  // 현재 스테이지 번호 (1, 2, 3, 4, 5)
    public int s1_nextindex;
    public GameObject startMessage;

    public bool Clear = false; // 디버깅용

    DataController datacontroller;
    StageClearController stageClearController;

    private void Start()
    {
        datacontroller = FindObjectOfType<DataController>();
        stageClearController = FindObjectOfType<StageClearController>();
    }

    void Update() // 디버깅용
    {
        if (Clear)
        {
            CompletePuzzle();
            GameManager.instance.isSecondLoad = true;
            Clear = false;
        }
    }

    // 퍼즐이 완료될 때마다 호출되는 메서드
    public void CompletePuzzle()
    {
        if (stageClearController != null)
            stageClearController.ClearStage();
        
        if (stageNumber == 1)
        {
            PlayerPosData.pos = new Vector3(-8.15f, -1.6f, 0);
            FadeManager.instance.ChangeScene(s1_nextindex);
            return;
        }
        else
        {
            if (!datacontroller.gameData.puzzle1Clear)
            {
                datacontroller.gameData.puzzle1Clear = true;
                Debug.Log(datacontroller.gameData.puzzle1Clear);
            }
            else if (!datacontroller.gameData.puzzle2Clear)
            {
                datacontroller.gameData.puzzle2Clear = true;
                Debug.Log(datacontroller.gameData.puzzle2Clear);
            }
            else if (!datacontroller.gameData.puzzle3Clear)
            {
                datacontroller.gameData.puzzle3Clear = true;
                Debug.Log(datacontroller.gameData.puzzle3Clear);
            }
        }
    }

    public void HideStartUI()
    {
        if (startMessage != null) startMessage.SetActive(false);
    }
}