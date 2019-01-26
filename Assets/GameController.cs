using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Text bubbleText;
    public string[] replycs;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void Bubble(int indexReplyc, float secondsDisapear)
    {
        StopCoroutine(BubbleCoroutine(indexReplyc, secondsDisapear));
        StartCoroutine(BubbleCoroutine(indexReplyc, secondsDisapear));
    }

    IEnumerator BubbleCoroutine(int indexReplyc, float secondsDisapear)
    {
        bubbleText.gameObject.SetActive(true);
        bubbleText.text = replycs[indexReplyc];

        yield return new WaitForSeconds(secondsDisapear);

        bubbleText.gameObject.SetActive(false);
    }
}
