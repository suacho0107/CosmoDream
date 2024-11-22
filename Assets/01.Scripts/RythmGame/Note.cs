using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Note �����տ� ����
public class Note : MonoBehaviour
{
    //�� �� ������ ��
    public float noteSpeed = 1200f;

    private Image noteImage;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        noteImage = GetComponent<Image>();
    }

    void Update()
    {
        transform.localPosition -= Vector3.right * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        StartCoroutine(JudgementNote());
    }

    IEnumerator JudgementNote()
    {
        //�ִϸ��̼� ���
        animator.SetBool("isJud", true);
        yield return new WaitForSeconds(0.5f);
        noteImage.enabled = false;
    }
}
