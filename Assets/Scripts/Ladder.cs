using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public static Ladder instance;

    public bool onLadder = false;
    public bool inProgress = false;
    public bool nearTop;
    public bool nearBottom;
    [SerializeField] private float speed = 4;

    [SerializeField] private Transform ladderTop;
    [SerializeField] private Transform ladderBottom;

    private void Awake()
    {
        instance = this;
    }

    public void Control()
    {
        var v = Player.instance.transform.position;
        
        inProgress = !nearBottom && !nearTop;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        v.y += vertical;
        if (vertical != 0)
            Player.instance.UsedWS = true;


        nearTop = v.y >= ladderTop.position.y - 0.00001;
        nearBottom = v.y <= ladderBottom.position.y + 0.00001;

        if (nearTop)
            v.y = ladderTop.position.y;
        
        Player.instance.transform.position = v;
    }

    void OnTriggerEnter2D()
    {
        onLadder = true;
    }

    void OnTriggerExit2D()
    {
        onLadder = false;
    }
}
