using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player�� ����

public class MovementManager : MonoBehaviour
{
    //�� �� ������Ʈ ����
    public GameObject leftMap;
    public GameObject centerMap;
    public GameObject rightMap;
    public GameObject backMap;

    public CanvasGroup fadePanel; //���̵� �г�

    public int pos = 25; //�� �̵� �� ��ġ(���� ����)
    public bool useFadeOnStart = true;

    private bool isMove; //�� �̵� �÷���
    private string scanMoveTag; //��ĵ�� �̵����� �±�

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
        //�ʱ� ����
        if (PlayerPosData.room == null)
        {
            PlayerPosData.room = centerMap;
        }
        ActivateOnly(PlayerPosData.room);

        fadeController = FindObjectOfType<FadeController>();
        playerController = FindObjectOfType<PlayerController>();
        yunohMove = FindObjectOfType<YunohMove>();
        fadePanel.alpha = 0f;

        // �� ���� �� ���̵� ����� ����� ���
        if (useFadeOnStart)
        {
            //StartCoroutine(StartSceneFade());
        }

    }

    private void Update()
    {
        if (isMove) //�̵� ���� ��
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
        //�÷��̾� �̵�
        Vector3 playerPosition = transform.position;
        playerPosition.x = _pos;
        transform.position = playerPosition;
        //ī�޶� �̵�
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
        // �� ���� �� ���̵�ƿ� ȿ��
        yield return StartCoroutine(fadeController.FadeOut(fadePanel));
    }
}
