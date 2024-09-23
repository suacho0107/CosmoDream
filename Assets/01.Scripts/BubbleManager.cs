using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubbleCanvas;
    public GameObject bubblePrefab;

    Queue<GameObject> bubblePool = new Queue<GameObject>();
    TalkManager talkManager;
    
    public int bubbleIndex; // 대화 인덱스
    public bool isBubble; // 말풍선 표시 여부
    int id;
    GameObject scanObject;

    void Start()
    {
        InitBubblePool(5);
        talkManager = FindObjectOfType<TalkManager>();
    }

    void InitBubblePool(int count)
    {
        Transform bubbleCanvasTransform = bubbleCanvas.transform;

        for (int i = 0; i < count; i++)
        {
            var bubble = Instantiate(bubblePrefab);
            bubble.transform.SetParent(bubbleCanvasTransform, false);
            bubble.SetActive(false);
            bubblePool.Enqueue(bubble);
        }
    }

    public void StartBubbleInteraction(GameObject scanObj, int id)
    {
        scanObject = scanObj;
        this.id = id;
        bubbleIndex = 0;
        StartCoroutine(BubbleCoroutine());
    }

    IEnumerator BubbleCoroutine()
    {
        if (isBubble) yield break;

        while (true)
        {
            string talkData = talkManager.GetTalk(id, bubbleIndex);

            if (talkData == null)
            {
                bubbleIndex = 0;
                isBubble = false;
                yield break;
            }

            // 프리팹을 풀에서 가져옴
            var bubble = bubblePool.Dequeue();
            bubble.SetActive(true);
            // 텍스트 설정
            var bubbleText = bubble.GetComponentInChildren<Text>();
            bubbleText.text = talkData;
            // scanObject의 위치를 기준으로 말풍선을 생성하여 배치
            Transform scanTransform = scanObject.GetComponent<Transform>();
            bubble.transform.position = scanTransform.position + new Vector3(0, 2.5f, 0);
            
            isBubble = true;
            yield return new WaitForSeconds(2f);
            bubble.SetActive(false);
            bubblePool.Enqueue(bubble);

            bubbleIndex++;
        }
    }
}