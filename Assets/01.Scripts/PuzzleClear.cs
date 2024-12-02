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
        GameManager.instance.completedPuzzles++;

        // 모든 퍼즐이 완료되었는지 확인
        if (GameManager.instance.completedPuzzles >= totalPuzzles)
        {
            OnStageComplete();
        }
        else
        {
            FadeManager.instance.ChangeScene(nextSceneName); // 다음 퍼즐 씬으로 이동
            GameManager.instance.isSecondLoad = true;
            Debug.Log($"현재 완료된 퍼즐 수: {GameManager.instance.completedPuzzles}/{totalPuzzles}");  // 퍼즐 개수 확인용 로그
        }
    }

    // 스테이지가 완료되었을 때 호출
    public void OnStageComplete()
    {
        Debug.Log("모든 퍼즐 완료, OnStageComplete 호출됨");
        GameManager.instance.isSecondLoad = true;
        GameManager.instance.completedPuzzles = 0; // 퍼즐 완료 상태 초기화

        FadeManager.instance.ChangeScene(originalSceneName);
        // 스테이지별 완료 처리 (필요하다면..)
        // if (stageNumber == 1 || stageNumber == 2)
    }

    public void HideStartUI()
    {
        if (startMessage != null) startMessage.SetActive(false);
    }
}