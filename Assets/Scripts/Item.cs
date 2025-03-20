using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemName;

    public int GetIndex()
    {
        
        TrackingFinances tf = GameObject.Find("DailyFinanceTracker").GetComponent<TrackingFinances>();
        int index=-1;
        for(int i=0; i<tf.products.Count; i++)
        {
            if (tf.products[i].ProductName == ItemName)
            {
                index = i; break;
            }
        }
        return index;
    }
}
