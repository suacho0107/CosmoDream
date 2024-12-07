using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ypMove : MonoBehaviour
{
    public GameObject player;  // 플레이어 오브젝트
    public GameObject yunoh;   // 유노 오브젝트

    public Vector3 yunohTargetPosition = new Vector3(27.48f, -1.592959f, 0f);  // 유노의 목표 위치
    public Vector3 playerTargetPosition = new Vector3(20.09f, -1.592959f, 0f);
    
    public float stopDistance = 0.5f; // 유노가 정지할 거리

    private Animator yunohAnimator;
    private Animator playerAnimator;

    void Start()
    {
        yunohAnimator = yunoh.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
    }

    public void StartMovement()
    {
        // Vector3 scale = yunoh.transform.localScale;
        // scale.x = -Mathf.Abs(scale.x);
        // yunoh.transform.localScale = scale;
        SetDirection(yunoh, true);  // 유노는 오른쪽이 -1
        SetDirection(player, false); // 플레이어는 오른쪽이 1
        StartCoroutine(moveYunoh());
    }

    private IEnumerator moveYunoh()
    {
        // 유노 이동 시작 - 애니메이션 활성화
        yunohAnimator.SetBool("isWalking", true);

        // 플레이어 이동 시작
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(movePlayer());

        while (Vector3.Distance(yunoh.transform.position, yunohTargetPosition) > stopDistance)
        {
            yunoh.transform.position = Vector3.MoveTowards(
                yunoh.transform.position,
                yunohTargetPosition,
                6.0f * Time.deltaTime
            );
            yield return new WaitForFixedUpdate();
        }

        yunohAnimator.SetBool("isWalking", false);
    }

    private IEnumerator movePlayer()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.SetMove(false);
        }
        playerAnimator.SetBool("isWalking", true); // 걷는 애니메이션 활성화

        while (Vector3.Distance(player.transform.position, playerTargetPosition) > stopDistance)
        {
            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                playerTargetPosition,
                5.0f * Time.deltaTime
            );

            if (!playerAnimator.GetBool("isWalking")) 
            {
                playerAnimator.SetBool("isWalking", true); // 매 프레임 애니메이션 활성화 확인
            }
            yield return null;
        }

        playerController.SetMove(true);
    }

    private void SetDirection(GameObject character, bool isYunoh)
    {
        Vector3 scale = character.transform.localScale;

        if (isYunoh)
        {
            // 유노: 오른쪽이 -1
            scale.x = -Mathf.Abs(scale.x);
        }
        else
        {
            // 플레이어: 오른쪽이 1
            scale.x = Mathf.Abs(scale.x);
        }

        character.transform.localScale = scale;
    }
}