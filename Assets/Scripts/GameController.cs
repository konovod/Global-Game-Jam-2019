using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;


    public GameObject task;
    public SpriteRenderer bubble;
    public Sprite[] replycs;
    int curReplic = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StopBubble();
    }
    
    void Update()
    {
        
    }

    public void StartBubble(int indexReplyc, float secondsDisapear)
    {
        curReplic = indexReplyc;
        bubble.sprite = replycs[indexReplyc];
        task.SetActive(true);

        StopCoroutine(BubbleCoroutine(indexReplyc, secondsDisapear));
        //StartCoroutine(BubbleCoroutine(indexReplyc, secondsDisapear));
    }

    public void StopBubble()
    {
        curReplic = -1;
        task.SetActive(false);
    }

    IEnumerator BubbleCoroutine(int indexReplyc, float secondsDisapear)
    {
        if(secondsDisapear <= 0)
            yield return new WaitForSeconds(secondsDisapear);

        if(indexReplyc == curReplic && secondsDisapear >= 0)
            task.SetActive(false);
    }
}
