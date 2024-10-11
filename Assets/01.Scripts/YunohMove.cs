using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YunohMove : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float followDistance = 10.0f; // 플레이어와의 거리
    public float followSpeed = 5.0f; // 따라오는 속도

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > followDistance)
        {
            // 목표 위치 계산 
            Vector3 targetPosition = player.position + (transform.position - player.position).normalized * followDistance;
            
            // 부드러운 이동 (최대 속도로 이동하지 않도록 followSpeed로 제한)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // 애니메이션 활성화
            animator.SetBool("isWalking", true);
        }
        else
        {
            // 애니메이션 비활성화 (정지 상태)
            animator.SetBool("isWalking", false);
        }

        // 플레이어를 바라보도록 방향 조정
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


    public void PositionBehindPlayer(int dir)
    {
        // 플레이어의 이동 방향에 따라 뒤쪽 위치로 이동
        Vector3 behindPosition = player.position - (player.right * followDistance * dir);
        behindPosition.y = player.position.y; // y축을 플레이어와 동일하게 맞춤
        transform.position = behindPosition;
    }
}
