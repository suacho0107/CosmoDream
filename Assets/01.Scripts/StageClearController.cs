using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//하이어라키에 빈 오브젝트 만들고 이 스크립트 넣어주세요
//public 값 전부 설정해주기

public class StageClearController : MonoBehaviour
{
    public string NowStgScene; //스테이지 2이면 스테이지2 씬이름 넣기
    public string NextStgScene; //스테이지 2이면 스테이지3 씬이름 넣기
    public Vector3 initPosition = new Vector3(-9.3f, -1.6f, 0); //스테이지 2이면 스테이지3 초기 플레이어 위치 넣기

   DataController datacontroller;

    private void Start()
    {
        datacontroller = DataController.Instance;
    }

    public void ClearStage()
    {
        Debug.Log($"퍼즐 상태: puzzle1Clear={datacontroller.gameData.puzzle1Clear}, puzzle2Clear={datacontroller.gameData.puzzle2Clear}, puzzle3Clear={datacontroller.gameData.puzzle3Clear}");

        if (datacontroller.gameData.puzzle1Clear &&
            datacontroller.gameData.puzzle2Clear &&
            datacontroller.gameData.puzzle3Clear)
        {
            Debug.Log("모든 퍼즐 완료 확인됨. 다음 스테이지로 이동.");

            //클리어 체크할 변수 = true;
            //다음 스테이지로 넘어왔으면 ^꺼 다시 false로 변경

            //게임 껐다 켰을 때 시작될 스테이지 변경
            datacontroller.gameData.nowStg++;

            //플레이어 위치 초기값 넣어주기
            PlayerPosData.pos = initPosition;
            PlayerPosData.romInx = 0;

            //씬로드(NextStgScene)
            FadeManager.instance.ChangeScene(NextStgScene);
            GameManager.instance.isSecondLoad = false;
            Debug.Log("다음 스테이지로 돌아갑니다. (" + NextStgScene + ")");

            //퍼즐 클리어 여부 되돌리기
            datacontroller.gameData.puzzle1Clear = false;
            datacontroller.gameData.puzzle2Clear = false;
            datacontroller.gameData.puzzle3Clear = false;
        }

        else
        {
            //씬로드(NowStgScene)
            FadeManager.instance.ChangeScene(NowStgScene);
            GameManager.instance.isSecondLoad = true;

            Debug.Log("퍼즐 완료되지 않음, 기존 스테이지로 돌아갑니다. (" + NowStgScene + ")");
        }
    }
}
