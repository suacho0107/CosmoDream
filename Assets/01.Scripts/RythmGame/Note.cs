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
        // �� ������ ������ �� �������� �̵�
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
        // �ʿ��ϴٸ� ���� ������Ʈ Ǯ�� ��ȯ�ϴ� ���� �۾� ����
    }
}
