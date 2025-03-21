using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouse : MonoBehaviour
{
    public StoragePoint[] WareHousePoints;
    public GameObject[] AvailableItems;
    public void Reload(int amount, TrackingFinances tf)
    {
        foreach(StoragePoint storagePoint in WareHousePoints)
        {
            for(int i = 0; i < amount; i++)
            {
                GameObject currentObj = Instantiate(AvailableItems[Random.Range(0, tf.products.Count)]);
                storagePoint.AddItem(currentObj);
            }
        }
    }
}
