using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 defaultScale;
    float scaleMultiplier = 1.2f;
    float AlphaThreshold = 0.1f;

    Button btn;

    private void Start()
    {
        //this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        defaultScale = transform.localScale;
        btn = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            transform.localScale = defaultScale * scaleMultiplier;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            transform.localScale = defaultScale;
        }
    }
}
