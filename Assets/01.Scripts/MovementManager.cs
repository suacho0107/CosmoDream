using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player에 적용

public class MovementManager : MonoBehaviour
{
    //맵 당 오브젝트 묶음
    public GameObject leftMap;
    public GameObject centerMap;
    public GameObject rightMap;
    public GameObject backMap;

    public CanvasGroup fadePanel; //페이드 패널

    public int pos = 25; //맵 이동 후 위치(각자 조절)
    public bool useFadeOnStart = true;

    private bool isMove; //맵 이동 플래그
    private string scanMoveTag; //스캔된 이동방향 태그

    private FadeController fadeController;
    private PlayerController playerController;
    YunohMove yunohMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isMove = true;
        scanMoveTag = collision.gameObject.tag;
    }

    private void Start()
    {
        //초기 설정
        if (PlayerPosData.room == null)
        {
            PlayerPosData.room = centerMap;
        }
        ActivateOnly(PlayerPosData.room);

        fadeController = FindObjectOfType<FadeController>();
        playerController = FindObjectOfType<PlayerController>();
        yunohMove = FindObjectOfType<YunohMove>();
        fadePanel.alpha = 0f;

        // 씬 시작 시 페이드 기능을 사용할 경우
        if (useFadeOnStart)
        {
            //StartCoroutine(StartSceneFade());
        }

    }

    private void Update()
    {
        if (isMove) //이동 진입 시
        {
            switch (scanMoveTag)
            {
                case "Right":
                    //MoveToRight();
                    StartCoroutine(FadeAndMove(pos, 1));
                    break;
                case "Left":
                    //MoveToLeft();
                    StartCoroutine(FadeAndMove(pos, -1));
                    break;
                default:
                    break;
            }
        }

        isMove = false;
        scanMoveTag = null;
    }

    private void ActivateOnly(GameObject toActive)
    {
        leftMap.SetActive(false);
        centerMap.SetActive(false);
        rightMap.SetActive(false);
        backMap.SetActive(false);

        toActive.SetActive(true);
        PlayerPosData.room = toActive;
    }

    private void movePlayer(int _pos)
    {
        //플레이어 이동
        Vector3 playerPosition = transform.position;
        playerPosition.x = _pos;
        transform.position = playerPosition;
        //카메라 이동
        Camera.main.transform.position = new Vector3(_pos, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    private IEnumerator FadeAndMove(int _pos, int dir)
    {
        playerController.SetMove(false);
        fadePanel.alpha = 1f;
        yield return StartCoroutine(fadeController.FadeIn(fadePanel));

        if(dir == -1) //left
        {
            movePlayer(_pos);

            if (leftMap.activeSelf)
            {
                ActivateOnly(backMap);
            }
            else if (centerMap.activeSelf)
            {
                ActivateOnly(leftMap);
            }
            else if (rightMap.activeSelf)
            {
                ActivateOnly(centerMap);
            }
            else if (backMap.activeSelf)
            {
                ActivateOnly(rightMap);
            }
        }
        else if(dir == 1) //right
        {
            movePlayer(-_pos);

            if (leftMap.activeSelf)
            {
                ActivateOnly(centerMap);
            }
            else if (centerMap.activeSelf)
            {
                ActivateOnly(rightMap);
            }
            else if (rightMap.activeSelf)
            {
                ActivateOnly(backMap);
            }
            else if (backMap.activeSelf)
            {
                ActivateOnly(leftMap);
            }
        }
        if (yunohMove != null)
        {
            yunohMove.PositionBehindPlayer(dir);
        }

        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(fadeController.FadeOut(fadePanel));
        playerController.SetMove(true);
    }
    private IEnumerator StartSceneFade()
    {
        // 씬 시작 시 페이드아웃 효과
        yield return StartCoroutine(fadeController.FadeOut(fadePanel));
    }
}
