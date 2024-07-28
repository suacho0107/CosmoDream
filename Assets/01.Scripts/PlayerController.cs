using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;

    public bool isMove = true;
    public float movePower = 6.0f;
    public float interactDistance = 0.6f;
    private List<Interactable> Interactables = new List<Interactable>();

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isMove)
        {
            Move();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpacebarInteract();
        }
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("isWalking", true);
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isWalking", true);
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void SpacebarInteract()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactDistance);
        Interactables.Clear();
        foreach (var collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                Interactables.Add(interactable);
            }
        }

        foreach (var interactable in Interactables)
        {
            interactable.Interact();
        }
    }

    // ����� �뵵, �浹���� �ð��� ǥ�� (Hierarchyâ���� Player �����ϸ� �� �� ����)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }

    // �ܺο��� isMove ���� ������ ���� �޼���
    public void SetMove(bool isMove)
    {
        isMove = this.isMove;
    }
}
