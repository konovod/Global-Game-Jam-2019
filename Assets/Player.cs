using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private bool hands = false;
    [SerializeField] private Transform handsPosition;
    [SerializeField] private Transform dropPosition;
    [SerializeField] private GameObject currentItem;
    [SerializeField] private List<Transform> nearest = new List<Transform>();

    
    void Update()
    {
        Move();
        TakeAndPutItem();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(Vector3.right * horizontal);
    }

    void TakeAndPutItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hands)
            {
                //Take
                if (nearest.Count > 0)
                {
                    int rand = Random.Range(0, nearest.Count);

                    currentItem = nearest[rand].gameObject;
                    currentItem.transform.SetParent(handsPosition);
                    currentItem.transform.position = handsPosition.position;

                    nearest.Remove(nearest[rand]);

                    hands = true;
                }
            }
            else
            {
                //Drop
                GameObject drop = Instantiate(currentItem, dropPosition.position, transform.rotation);
                drop.name = currentItem.name;
                drop.transform.localScale = 
                    new Vector3(drop.transform.localScale.x, drop.transform.localScale.y * 2, 
                    drop.transform.localScale.z);
                Destroy(currentItem);
                currentItem = null;

                hands = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            nearest.Add(col.transform.parent);
            Debug.Log("Премет: " + col.name);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            nearest.Remove(col.transform.parent);
            Debug.Log("Премет: " + col.name);
        }
    }
}
