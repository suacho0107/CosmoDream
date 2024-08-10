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
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dirVec = Vector2.right; // �⺻ ���� ������
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
        {
            manager.Action(scanObject); // �����̽��� ��ȣ�ۿ�
        }

        if (manager.isAction)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        if (!manager.isAction)
        {
            isMove = true;
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

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("isWalking", true);
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            dirVec = Vector2.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isWalking", true);
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
            dirVec = Vector2.right;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    // �ܺο��� isMove ���� ������ ���� �޼���
    public void SetMove(bool canMove)
    {
        isMove = canMove;
    }
}