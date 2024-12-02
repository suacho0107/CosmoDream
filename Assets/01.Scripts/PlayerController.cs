using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator animator;
    public float movePower = 6.0f;
    public bool isMove;

    GameManager manager;
    TalkManager talkManager;
    GameObject scanObject;
    RaycastHit2D rayHit;
    Vector3 dirVec;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        talkManager = FindObjectOfType<TalkManager>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dirVec = Vector2.right; // 기본 방향 오른쪽
        isMove = true;

        //좌표 불러오기
        if (PlayerPosData.pos != null)
        {
            Debug.Log(PlayerPosData.pos);
            transform.position = PlayerPosData.pos;
        }
    }

    void Update()
    {
        Debug.Log(PlayerPosData.pos);
        Debug.Log(transform.position);
        if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
        {
            manager.Action(scanObject); // 스페이스바 상호작용
            //위치 저장
            PlayerPosData.pos = transform.position;
            Debug.Log(PlayerPosData.pos);
        }

        if (manager.isTalk)
        {
            isMove = false;
        }

        if (!isMove)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        if (isMove)
        {
            Move();
        }

        Debug.DrawRay(rigid.position, dirVec * 1.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftShift)) // Shift키로 달리기
        {
            movePower = 10.0f;
        }
        else
        {
            movePower = 6.0f;
        }
        
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
            dirVec = Vector2.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
            dirVec = Vector2.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
        animator.SetBool("isWalking", moveVelocity != Vector3.zero);
    }

    // 외부에서 isMove 변수 설정을 위한 메서드
    public void SetMove(bool canMove)
    {
        isMove = canMove;
    }
    
}