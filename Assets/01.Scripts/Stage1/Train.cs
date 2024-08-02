using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject train;
    public float trainSpeed = 1.0f;
    public float rightLimit = 14.0f;

    void Start()
    {
        if (train == null)
        {
            Debug.LogError("Train 오브젝트가 할당되지 않았습니다.");
            return;
        }
    }

    void Update()
    {
        if (train == null) return;

        train.transform.Translate(Vector3.right * trainSpeed * Time.deltaTime);

        if (train.transform.position.x > rightLimit)
        {
            train.transform.position = new Vector3(rightLimit, train.transform.position.y, train.transform.position.z);
            trainSpeed = 0;
        }
    }
}