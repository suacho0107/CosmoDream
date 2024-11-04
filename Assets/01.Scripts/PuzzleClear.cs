using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleClear : MonoBehaviour
{
    public int stageNumber;  // 현재 스테이지 번호 (1, 2, 3, 4, 5)
    public int totalPuzzles; // 해당 스테이지의 퍼즐 개수
    public string nextSceneName; // 다음으로 이동할 씬 이름
    public string originalSceneName; // 모든 퍼즐 다 풀었을때 돌아갈 씬
    ObjData objData; // 대사 변경을 위한 ObjData 참조

    public bool Clear=false; // 디버깅용

    void Start()
    {
        objData = FindObjectOfType<ObjData>();
    }

    void Update() // 디버깅용
    {
        if (Clear)
        {
            CompletePuzzle();
            GameManager.instance.isSecondLoad = true;
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
            Debug.Log("모든 퍼즐이 완료되었습니다");
        }
        else
        {
            SceneManager.LoadScene(nextSceneName); // 다음 퍼즐 씬으로 이동
            Debug.Log($"현재 완료된 퍼즐 수: {GameManager.instance.completedPuzzles}/{totalPuzzles}");  // 퍼즐 개수 확인용 로그
        }
    }

    // 스테이지가 완료되었을 때 호출
    public void OnStageComplete()
    {
        Debug.Log("OnStageComplete 호출됨");
        // 스테이지별 완료 처리 (필요하다면..)
        if (stageNumber == 1 || stageNumber == 2)
        {
            GameManager.instance.isSecondLoad = true;
            Debug.Log("isSecondLoad가 true로 설정되었습니다.");
        }

        // 원래 씬으로 이동
        SceneManager.LoadScene(originalSceneName);
    }
}