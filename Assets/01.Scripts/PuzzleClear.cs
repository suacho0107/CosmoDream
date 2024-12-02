using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleClear : MonoBehaviour
{
    public int stageNumber;  // 현재 스테이지 번호 (1, 2, 3, 4, 5)
    public int totalPuzzles; // 해당 스테이지의 퍼즐 개수
    public string nextSceneName; // 다음으로 이동할 씬 이름
    public string originalSceneName; // 모든 퍼즐 다 풀었을때 돌아갈 씬
    ObjData objData; // 대사 변경을 위한 ObjData 참조
    public GameObject startMessage;

    public bool Clear=false; // 디버깅용

    void Start()
    {
        objData = FindObjectOfType<ObjData>();
        Clear = false;
    }

    void Update() // 디버깅용
    {
        if (Clear)
        {
            CompletePuzzle();
            GameManager.instance.isSecondLoad = true;
            Clear=false;
        }
    }

    // 퍼즐이 완료될 때마다 호출되는 메서드
    public void CompletePuzzle()
    {
        // 스테이지 번호에 따라 퍼즐 완료 상태를 갱신합니다.
        switch (stageNumber)
        {
            case 1:
                if (!GameData.puzzle1_1)
                    GameData.puzzle1_1 = true;
                else if (!GameData.puzzle1_2)
                    GameData.puzzle1_2 = true;
                else if (!GameData.puzzle1_3)
                    GameData.puzzle1_3 = true;
                else if (!GameData.puzzle1_4)
                    GameData.puzzle1_4 = true;
                break;

            case 2:
                if (!GameData.puzzle2_1)
                    GameData.puzzle2_1 = true;
                else if (!GameData.puzzle2_2)
                    GameData.puzzle2_2 = true;
                else if (!GameData.puzzle2_3)
                    GameData.puzzle2_3 = true;
                break;
        }
        
        if (IsStageComplete()) // 모든 퍼즐이 완료되었는지
        {
            OnStageComplete();
        }
        else
        {
            FadeManager.instance.ChangeScene(nextSceneName); // 다음 퍼즐 씬으로 이동
            GameManager.instance.isSecondLoad = true;
            Debug.Log($"현재 스테이지 {stageNumber} 퍼즐 완료 상태를 갱신하였습니다.");
        }
    }

    private bool IsStageComplete()
    {
        switch (stageNumber)
        {
            case 1:
                return GameData.puzzle1_1 && GameData.puzzle1_2 && GameData.puzzle1_3 && GameData.puzzle1_4;

            case 2:
                return GameData.puzzle2_1 && GameData.puzzle2_2 && GameData.puzzle2_3;

            default:
                return false;
        }
    }


    // 스테이지가 완료되었을 때 호출
    public void OnStageComplete()
    {
        Debug.Log("모든 퍼즐 완료, OnStageComplete 호출됨");
        GameManager.instance.isSecondLoad = true;

        FadeManager.instance.ChangeScene(originalSceneName);
    }

    public void HideStartUI()
    {
        if (startMessage != null) startMessage.SetActive(false);
    }
}