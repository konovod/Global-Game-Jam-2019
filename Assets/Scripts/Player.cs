using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    [SerializeField] private float speed = 4;
    [SerializeField] private bool hands = false;
    public Transform handsPosition;
    [SerializeField] private Transform dropPosition;
    public GameObject currentItem;
    [SerializeField] private List<Transform> nearest = new List<Transform>();
    [SerializeField] private List<Transform> nearestCells = new List<Transform>();
    private SpriteRenderer sr;
    public Sprite[] frames;

    public bool UsedAD;
    public bool UsedWS;

    private void Awake()
    {
        UsedAD = false;
        UsedWS = false;
        sr = GetComponent<SpriteRenderer>();
        instance = this;
    }

    void Update()
    {
        Move();
        TakeAndPutItem();
    }

    void Move()
    {
        if (Ladder.instance.onLadder)
        {
            Ladder.instance.Control();
        }
        if (!Ladder.instance.inProgress)
        {
            float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            if(horizontal != 0)
                UsedAD = true;
            transform.Translate(Vector3.right * horizontal);
        }
    }

    void TakeAndPutItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hands)
            {
                var currentCells = nearestCells.Where(cell => cell.GetComponent<Cell>().filled).ToArray();
                if (currentCells.Count() > 0)
                {
                    currentCells[0].GetComponent<Cell>().Take();
                    
                    hands = true;
                }
                else
                {
                    Pick();
                }
            }
            else
            {
                if (!Ladder.instance.inProgress)
                {
                    var currentCells = nearestCells.Where(cell => !cell.GetComponent<Cell>().filled).ToArray();
                    if (currentCells.Count() > 0)
                    {
                        PutInTheCell(currentCells[0].GetComponent<Cell>(), currentItem);
                    }
                    else
                    {
                        DropOnTheFloor();
                    }

                    hands = false;
                }
            }
        }
    }

    void Pick()
    {
        if (nearest.Count > 0)
        {
            int rand = Random.Range(0, nearest.Count);

            currentItem = nearest[rand].gameObject;
            nearest.Remove(nearest[rand].parent);
            currentItem.transform.SetParent(handsPosition);
            currentItem.transform.position = handsPosition.position;

            currentItem.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            currentItem.GetComponent<Item>().SetFit(null, false);
            if(Item.TRASH_IN_HAND)
                currentItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentItem.GetComponent<Item>().mistake;
            else
                currentItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentItem.GetComponent<Item>().normal;

            hands = true;
        }
    }

    void DropOnTheFloor()
    {
        GameObject drop = Instantiate(currentItem, dropPosition.position, currentItem.transform.rotation);
        drop.name = currentItem.name;
        //drop.transform.localScale =
        //    new Vector3(drop.transform.localScale.x, drop.transform.localScale.y,
        //    drop.transform.localScale.z);

        drop.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        currentItem.GetComponent<Item>().SetFit(drop.transform, false);
        drop.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentItem.GetComponent<Item>().mistake;

        Destroy(currentItem);
        currentItem = null;
    }

    void PutInTheCell(Cell cell, GameObject obj)
    {

        cell.Put(obj);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent)
        {
            if (col.transform.parent.GetComponent<Item>())
            {
                nearest.Add(col.transform.parent);
            }
            else if (col.transform.parent.GetComponent<Cell>())
            {
                nearestCells.Add(col.transform.parent);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.parent)
        {
            if (col.transform.parent.GetComponent<Item>())
            {
                nearest.Remove(col.transform.parent);
            }
            else if (col.transform.parent.GetComponent<Cell>())
            {
                nearestCells.Remove(col.transform.parent);
            }
        }
    }
}
