using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    //�� ���
    public Sprite Background; //���� ���
    public Sprite leftBG;
    public Sprite centerBG;
    public Sprite rightBG;
    public Sprite backBG;

    //�� �� ������Ʈ ����
    public GameObject leftMap;
    public GameObject centerMap;
    public GameObject rightMap;
    public GameObject backMap;

    private bool isMove; //�� �̵� �÷���
    private string scanMoveTag; //��ĵ�� �̵����� �±�

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
        //�ʱ� ����
        ActivateOnly(centerMap);
        ChangeBG(centerBG);
    }

    private void Update()
    {
        if (isMove) //�̵� ���� ��
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

    private void MoveToLeft() //�������� �̵�
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

        //�÷��̾� �̵�
        Vector3 playerPosition = transform.position;
        playerPosition.x = 25;
        transform.position = playerPosition;
        //ī�޶� �̵�
    }

    private void MoveToRight() //���������� �̵�
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

        //�÷��̾� �̵�
        Vector3 playerPosition = transform.position;
        playerPosition.x = -25;
        transform.position = playerPosition;
        //ī�޶� �̵�
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
