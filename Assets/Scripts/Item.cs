﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public Sprite normal;
    public Sprite mistake;
    public bool rubbish;
    static public bool TRASH_IN_HAND = true;
    public static Dictionary<int, bool> fit = new Dictionary<int, bool>();
    public static Dictionary<int, Transform> ItemsLocation = new Dictionary<int, Transform>();

    public static bool IsFit(int id)
    {
        if (!fit.ContainsKey(id))
        { 
            fit.Add(id, false);
            ItemsLocation.Add(id, null);
        }
        return fit[id];
    }


    public void SetFit(Transform who, bool isFit)
    {
        if(fit.ContainsKey(id))
            fit[id] = isFit;
        else
            fit.Add(id, isFit);

        if (ItemsLocation.ContainsKey(id))
            ItemsLocation[id] = who;
        else
            ItemsLocation.Add(id, who);

    }
}
