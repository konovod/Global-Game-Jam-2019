using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool filled = false;
    public bool trueItem = false;
    public bool rubbishBin = false;
    public int needID;
    public Transform pos;
    public GameObject item;

    public void Put(GameObject obj)
    {
        GameObject put = 
            Instantiate(obj, pos.position, obj.transform.rotation);
        put.name = obj.name;
        put.transform.localScale =
            new Vector3(put.transform.localScale.x, put.transform.localScale.y * 2,
            put.transform.localScale.z);

        item = put;

        if (obj.GetComponent<Item>().id == needID)
        {
            trueItem = true;
        }
        else if (obj.GetComponent<Item>().rubbish && rubbishBin)
        {
            trueItem = true;
        }
        else if (!obj.GetComponent<Item>().rubbish && rubbishBin)
        {
            trueItem = true;
        }
        else
        {
            trueItem = false;
        }

            Destroy(obj);
        Player.instance.currentItem = null;

        filled = true;
    }

    public void Take()
    {
        Player.instance.currentItem = item;
        Player.instance.currentItem.transform.SetParent(Player.instance.handsPosition);
        Player.instance.currentItem.transform.position = Player.instance.handsPosition.position;
        item = null;
        filled = false;
        trueItem = false;
    }
}
