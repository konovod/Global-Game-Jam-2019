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
        //put.transform.localScale =
        //    new Vector3(put.transform.localScale.x, put.transform.localScale.y,
        //    put.transform.localScale.z);
        put.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
        
        item = put;

        trueItem = rubbishBin || obj.GetComponent<Item>().id == needID;
        if(trueItem)
            put.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = put.GetComponent<Item>().normal;
        else
            put.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = put.GetComponent<Item>().mistake;
        obj.GetComponent<Item>().SetFit(put.transform, trueItem);
        Destroy(obj);
        Player.instance.currentItem = null;

        filled = true;
    }

    public void Take()
    {
        Player.instance.currentItem = item;
        Player.instance.currentItem.transform.SetParent(Player.instance.handsPosition);
        Player.instance.currentItem.transform.position = Player.instance.handsPosition.position;
        Player.instance.currentItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 4;
        Player.instance.currentItem.GetComponent<Item>().SetFit(null, false);
        if(Item.TRASH_IN_HAND)
            Player.instance.currentItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Player.instance.currentItem.GetComponent<Item>().mistake;
        else
            Player.instance.currentItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Player.instance.currentItem.GetComponent<Item>().normal;
        item = null;
        filled = false;
        trueItem = false;
    }
}
