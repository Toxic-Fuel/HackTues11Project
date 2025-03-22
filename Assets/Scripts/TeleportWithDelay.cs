using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TeleportWithDelay : MonoBehaviour
{
    
    public StoragePoint Stand;
    public float daysToDo = 0f;
    public WeeklyFinances wf;
    Collision collisiong;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "item")
        {
            collisiong = collision;
            Invoke("Add", 2);

        }
        
    }
    public void Add()
    {
        Stand.AddItem(collisiong.gameObject);
    }
}
