using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject task;
    public SpriteRenderer bubble;
    public Sprite[] replycs;

    void Start()
    {
        StopBubble();
    }
    
    void Update()
    {
        
    }

    public void StartBubble(int indexReplyc, float secondsDisapear)
    {
        StopCoroutine(BubbleCoroutine(indexReplyc, secondsDisapear));
        StartCoroutine(BubbleCoroutine(indexReplyc, secondsDisapear));
    }

    public void StopBubble()
    {
        task.SetActive(false);
    }

    IEnumerator BubbleCoroutine(int indexReplyc, float secondsDisapear)
    {
        bubble.sprite = replycs[indexReplyc];
        task.SetActive(true);

        yield return new WaitForSeconds(secondsDisapear);

        task.SetActive(false);
    }
}
