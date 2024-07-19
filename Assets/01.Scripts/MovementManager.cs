using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player�� ����

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

    public int pos = 25; //�� �̵� �� ��ġ

    private bool isMove; //�� �̵� �÷���
    private string scanMoveTag; //��ĵ�� �̵����� �±�

    private FadeController fadeController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isMove = true;
        scanMoveTag = collision.gameObject.tag;
    }

    private void Start()
    {
        //�ʱ� ����
        //ActivateOnly(centerMap);
        //ChangeBG(centerBG);

        fadeController = FindObjectOfType<FadeController>();
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

        StartCoroutine(FadeAndMove(pos));
    }

    private void MoveToRight() //���������� �̵�
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
        //�÷��̾� �̵�
        Vector3 playerPosition = transform.position;
        playerPosition.x = _pos;
        transform.position = playerPosition;
        //ī�޶� �̵�
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
