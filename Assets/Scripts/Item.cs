using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public Sprite normal;
    public Sprite mistake;
    public bool rubbish;
    public static Dictionary<int, bool> fit = new Dictionary<int, bool>();

    public void SetFit(bool isFit)
    {
        if(!fit.ContainsKey(id))
            fit.Add(id, isFit);
        else
            fit[id] = isFit;
    }
}
