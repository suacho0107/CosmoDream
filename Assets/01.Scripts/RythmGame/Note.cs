using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Note 프리팹에 저장
public class Note : MonoBehaviour
{
    //노트 속도
    public float noteSpeed = 400f;

    private Image noteImage;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        noteImage = GetComponent<Image>();
    }

    void Update()
    {
        //노트 이동
        transform.localPosition -= Vector3.right * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        StartCoroutine(JudgementNote());
    }

    IEnumerator JudgementNote()
    {
        //애니메이션 재생
        animator.SetBool("isJud", true);
        yield return new WaitForSeconds(0.5f);
        noteImage.enabled = false;
    }
}
