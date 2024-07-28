using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float backgroundSpeed = 2.0f; // ��� ��ũ�� �ӵ�
    public float groundSpeed = 4.0f; // �ٴ� ��ũ�� �ӵ�
    public float trainSpeed = 6.0f; // ���� ��ũ�� �ӵ�
    public GameObject background; // ��� ������Ʈ
    public GameObject ground; // �ٴ� ������Ʈ
    public GameObject train; // ������ ������Ʈ

    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 deltaPosition = transform.position - previousPosition;

        // ��� ��ũ��
        background.transform.position += new Vector3(deltaPosition.x * backgroundSpeed, 0, 0);

        // �ٴ� ��ũ��
        ground.transform.position += new Vector3(deltaPosition.x * groundSpeed, 0, 0);

        // ������ ��ũ��
        train.transform.position += new Vector3(deltaPosition.x * trainSpeed, 0, 0);

        previousPosition = transform.position;
    }
}
