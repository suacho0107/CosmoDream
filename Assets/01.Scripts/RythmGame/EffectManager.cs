using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject perfectEffect;
    public GameObject greatEffect;
    public GameObject missEffect;
    public GameObject JudgEffect;

    public Transform upperEffectTf;
    public Transform lowerEffectTf;

    public void JudgementEffect(int num, string lane)
    {
        Transform spawnTf = lane == "upper" ? upperEffectTf : lowerEffectTf;
        GameObject effectInstance = null;

        // num 값에 따라 생성할 이펙트를 결정
        switch (num)
        {
            case 0:
                effectInstance = Instantiate(perfectEffect, spawnTf);
                break;
            case 1:
                effectInstance = Instantiate(greatEffect, spawnTf);
                break;
            case 2:
                effectInstance = Instantiate(missEffect, spawnTf);
                break;
        }
        Destroy(effectInstance, 1.0f);
    }
}
