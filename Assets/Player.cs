using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;


    [SerializeField] private float speed = 4;
    [SerializeField] private bool hands = false;
    public Transform handsPosition;
    [SerializeField] private Transform dropPosition;
    public GameObject currentItem;
    [SerializeField] private Cell currentCell;
    [SerializeField] private List<Transform> nearest = new List<Transform>();

    private void Awake()
    {
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
            transform.Translate(Vector3.right * horizontal);
        }
    }

    void TakeAndPutItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hands)
            {
                if (currentCell)
                {
                    if (currentCell.filled)
                    {
                        currentCell.Take();
                        Debug.Log("Взялся с ячейки");
                        hands = true;
                    }
                    else
                    {
                        Pick();
                    }
                }
                else
                {
                    Pick();
                }
            }
            else
            {
                if (currentCell)
                {
                    if (!currentCell.filled)
                        PutInTheCell(currentItem);
                    else
                        DropOnTheFloor();
                    Debug.Log("Положилось в ячейку");
                }
                else
                {
                    DropOnTheFloor();
                    Debug.Log("Выкинулось на пол");
                }

                hands = false;
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

            Debug.Log("Подобрался рандомный предмет");
            hands = true;
        }
    }

    void DropOnTheFloor()
    {
        GameObject drop = Instantiate(currentItem, dropPosition.position, currentItem.transform.rotation);
        drop.name = currentItem.name;
        drop.transform.localScale =
            new Vector3(drop.transform.localScale.x, drop.transform.localScale.y * 2,
            drop.transform.localScale.z);

        drop.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;

        Destroy(currentItem);
        currentItem = null;
    }

    void PutInTheCell(GameObject obj)
    {
        currentCell.Put(obj);
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
                currentCell = col.transform.parent.GetComponent<Cell>();
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
                currentCell = null;
            }
        }
    }
}
