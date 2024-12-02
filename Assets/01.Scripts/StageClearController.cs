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

    //DataController datacontroller;

    private void Start()
    {
        //datacontroller = FindObjectOfType<DataController>();
    }

    private void Update()
    {
        if(DataController.Instance.gameData.puzzle1Clear&& 
            DataController.Instance.gameData.puzzle2Clear&& 
            DataController.Instance.gameData.puzzle3Clear)
        {
            //퍼즐 클리어 여부 되돌리기
            DataController.Instance.gameData.puzzle1Clear = false;
            DataController.Instance.gameData.puzzle2Clear = false;
            DataController.Instance.gameData.puzzle3Clear = false;

            //게임 껐다 켰을 때 시작될 스테이지 변경
            DataController.Instance.gameData.nowStg++;

            //플레이어 위치 초기값 넣어주기
            PlayerPosData.pos = initPosition;

            //씬로드(NextStgScene)
            FadeManager.instance.ChangeScene(NextStgScene);
            GameManager.instance.isSecondLoad = true;
        }
        else
        {
            //씬로드(NowStgScene)
            FadeManager.instance.ChangeScene(NowStgScene);
            GameManager.instance.isSecondLoad = true;
        }
    }
}
