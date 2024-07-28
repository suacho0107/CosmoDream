using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject train;
    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 deltaPosition = transform.position - previousPosition;
        train.transform.position += new Vector3(deltaPosition.x * trainSpeed, 0, 0);
        previousPosition = transform.position;
    }
}
