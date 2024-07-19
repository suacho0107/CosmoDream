using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player에 적용

public class MovementManager : MonoBehaviour
{
    //맵 배경
    public Sprite Background; //현재 배경
    public Sprite leftBG;
    public Sprite centerBG;
    public Sprite rightBG;
    public Sprite backBG;

    //맵 당 오브젝트 묶음
    public GameObject leftMap;
    public GameObject centerMap;
    public GameObject rightMap;
    public GameObject backMap;

    public int pos = 25; //맵 이동 후 위치

    private bool isMove; //맵 이동 플래그
    private string scanMoveTag; //스캔된 이동방향 태그

    private FadeController fadeController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isMove = true;
        scanMoveTag = collision.gameObject.tag;
    }

    private void Start()
    {
        //초기 설정
        //ActivateOnly(centerMap);
        //ChangeBG(centerBG);

        fadeController = FindObjectOfType<FadeController>();
    }

    private void Update()
    {
        if (isMove) //이동 진입 시
        {
            switch (scanMoveTag)
            {
                case "Right":
                    MoveToRight();
                    break;
                case "Left":
                    MoveToLeft();
                    break;
                default:
                    break;
            }
        }

        isMove = false;
        scanMoveTag = null;
    }

    private void MoveToLeft() //왼쪽으로 이동
    {
        if (leftMap.activeSelf)
        {
            ActivateOnly(backMap);
            ChangeBG(backBG);
        }
        else if (centerMap.activeSelf)
        {
            ActivateOnly(leftMap);
            ChangeBG(leftBG);
        }
        else if (rightMap.activeSelf)
        {
            ActivateOnly(centerMap);
            ChangeBG(centerBG);
        }
        else if (backMap.activeSelf)
        {
            ActivateOnly(rightMap);
            ChangeBG(rightBG);
        }

        StartCoroutine(FadeAndMove(pos));
    }

    private void MoveToRight() //오른쪽으로 이동
    {
        /*
        if (leftMap.activeSelf)
        {
            ActivateOnly(centerMap);
            ChangeBG(centerBG);
        }
        else if (centerMap.activeSelf)
        {
            ActivateOnly(rightMap);
            ChangeBG(rightBG);
        }
        else if (rightMap.activeSelf)
        {
            ActivateOnly(backMap);
            ChangeBG(backBG);
        }
        else if (backMap.activeSelf)
        {
            ActivateOnly(leftMap);
            ChangeBG(leftBG);
        }
        */

        StartCoroutine(FadeAndMove(-pos));
    }

    private void ActivateOnly(GameObject toActive)
    {
        leftMap.SetActive(false);
        centerMap.SetActive(false);
        rightMap.SetActive(false);
        backMap.SetActive(false);

        toActive.SetActive(true);
    }

    private void ChangeBG(Sprite newBG)
    {
        if(newBG != null)
        {
            Background = newBG;
        }
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

    private IEnumerator FadeAndMove(int _pos)
    {
        yield return StartCoroutine(fadeController.FadeIn());

        movePlayer(_pos);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(fadeController.FadeOut());
    }
}
