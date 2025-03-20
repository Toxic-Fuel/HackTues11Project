using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePoint : MonoBehaviour
{
    GameObject workerSlot;
    public List<GameObject> items = new List<GameObject>();
    // Start is called before the first frame update
    public GameObject GetWorker()
    {
        return workerSlot;
    }
    public void AddItem(GameObject item)
    {
        item.transform.parent = null;
        item.transform.position = new Vector3(transform.position.x , transform.position.y+2f, transform.position.z);
        items.Add(item);
        if (item.GetComponent<Rigidbody>() != null)
        {
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
    public void RemoveItem(GameObject item)
    {
        items.Remove(item);
        item.transform.parent = null;
        if(item.GetComponent<Rigidbody>() != null)
        {
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    public GameObject GetItem()
    {
        return items[0];
    }
    public int GetItemCount()
    {
        return items.Count;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
