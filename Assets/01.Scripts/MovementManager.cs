using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isMove; //맵 이동 플래그
    private string scanMoveTag; //스캔된 이동방향 태그

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isMove = true;
        scanMoveTag = collision.gameObject.tag;
    }

    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        isMove = false;
        scanMoveTag = null;
    }
    */

    private void Start()
    {
        //초기 설정
        ActivateOnly(centerMap);
        ChangeBG(centerBG);
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

        //플레이어 이동
        Vector3 playerPosition = transform.position;
        playerPosition.x = 25;
        transform.position = playerPosition;
        //카메라 이동
    }

    private void MoveToRight() //오른쪽으로 이동
    {
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

        //플레이어 이동
        Vector3 playerPosition = transform.position;
        playerPosition.x = -25;
        transform.position = playerPosition;
        //카메라 이동
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
        Background = newBG;
    }
}
