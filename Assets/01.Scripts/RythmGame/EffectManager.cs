using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject perfectEffect;
    public GameObject greatEffect;
    public GameObject missEffect;

    public Transform upperEffectTf;
    public Transform lowerEffectTf;

    public void JudgementEffect(int num, string lane)
    {
        Transform spawnTf = lane == "upper" ? upperEffectTf : lowerEffectTf;


    }
}
