// PlayerController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    public float movePower = 6.0f;
    public bool isMove;

    public GameManager manager;
    private GameObject scanObject;
    private RaycastHit2D rayHit;
    Vector3 dirVec;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dirVec = Vector2.right; // 기본 방향 오른쪽
        isMove = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
        {
            manager.Action(scanObject); // 스페이스바 상호작용
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

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //animator.SetBool("isWalking", true);
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            dirVec = Vector2.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //animator.SetBool("isWalking", true);
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
            dirVec = Vector2.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enter"))
        {
            //manager.EnterTalk(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    // 외부에서 isMove 변수 설정을 위한 메서드
    public void SetMove(bool canMove)
    {
        isMove = canMove;
    }
}