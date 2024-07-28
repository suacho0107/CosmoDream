using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float backgroundSpeed = 2.0f; // 배경 스크롤 속도
    public float groundSpeed = 4.0f; // 바닥 스크롤 속도
    public float trainSpeed = 6.0f; // 기차 스크롤 속도
    public GameObject background; // 배경 오브젝트
    public GameObject ground; // 바닥 오브젝트
    public GameObject train; // 기차역 오브젝트

    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 deltaPosition = transform.position - previousPosition;

        // 배경 스크롤
        background.transform.position += new Vector3(deltaPosition.x * backgroundSpeed, 0, 0);

        // 바닥 스크롤
        ground.transform.position += new Vector3(deltaPosition.x * groundSpeed, 0, 0);

        // 기차역 스크롤
        train.transform.position += new Vector3(deltaPosition.x * trainSpeed, 0, 0);

        previousPosition = transform.position;
    }
}
