using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Note : MonoBehaviour
{
    [SerializeField] private float noteSpeed = 1200f;
    private Image noteImage;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        noteImage = GetComponent<Image>();
    }

    private void Update()
    {
        // 매 프레임 오른쪽 → 왼쪽으로 이동
        transform.localPosition -= Vector3.right * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        StartCoroutine(JudgementNote());
    }

    private IEnumerator JudgementNote()
    {
        animator.SetBool("isJud", true);
        yield return new WaitForSeconds(0.5f);
        noteImage.enabled = false;
        // 필요하다면 이후 오브젝트 풀로 반환하는 등의 작업 가능
    }
}
