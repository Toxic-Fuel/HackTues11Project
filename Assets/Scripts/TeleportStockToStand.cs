using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TeleportStockToStand : MonoBehaviour
{
    
    public StoragePoint Stand;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "item")
        {
            Stand.AddItem(collision.gameObject);

        }
    }
}
