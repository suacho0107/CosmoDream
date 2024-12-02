using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmount = 0.03f;
    //public Canvas canvas;
    float ShakeTime;
    Vector3 initialPosition;

    public void VibrateForTime(float time)
    {
        ShakeTime = time;
        //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        //canvas.renderMode = RenderMode.WorldSpace;
    }

    private void Start()
    {
        initialPosition = new Vector3(0f, 0f, -10f);
    }
    private void Update()
    {
        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
            ShakeTime -= Time.unscaledDeltaTime;
        }
        else
        {
            ShakeTime = 0f;
            transform.position = initialPosition;
            //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        }
    }
}
