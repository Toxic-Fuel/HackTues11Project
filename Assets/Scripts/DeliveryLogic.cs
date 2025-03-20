using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryLogic : MonoBehaviour
{
    public StoragePoint PickUpPlace;
    public StoragePoint PutDownPlace;
    public string currentTask;
    NavMeshAgent navAgent;
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void ChooseTask(string task)
    {
        switch (task)
        {
            case "Deliver":

                break;
            case "FreeTime":

                break;
        }
    }

    void Deliver()
    {
       //find available storages
        List<StoragePoint> availablePickUpPoints = new List<StoragePoint>();
        List<StoragePoint> availableDropOffPoints = new List<StoragePoint>();
        GameObject[] ObjsContainingTagPickUp = GameObject.FindGameObjectsWithTag("pickup");
        GameObject[] ObjsContainingTagDropOff = GameObject.FindGameObjectsWithTag("dropff");
        //check if points are taken already
        foreach(GameObject obj in ObjsContainingTagPickUp)
        {
            StoragePoint curStorage = obj.GetComponent<StoragePoint>();
            if (curStorage.GetWorker() == null)
            {
                availablePickUpPoints.Add(curStorage);
            }
        }
        foreach (GameObject obj in ObjsContainingTagDropOff)
        {
            StoragePoint curStorage = obj.GetComponent<StoragePoint>();
            if (curStorage.GetWorker() == null)
            {
                availableDropOffPoints.Add(curStorage);
            }
        }
        //assign the 2 points
        PickUpPlace = availablePickUpPoints[Random.Range(0, availablePickUpPoints.Count)];
        PutDownPlace = availableDropOffPoints[Random.Range(0, availableDropOffPoints.Count)];


        

    }
    //Transfer process
    private IEnumerable Transfer(StoragePoint pickUpPoint, StoragePoint putDownPoint)
    {
        //go to pickup point
        navAgent.Move(pickUpPoint.gameObject.transform.position);
        //don't continue until its close enough
        while (Vector3.Distance(pickUpPoint.gameObject.transform.position, gameObject.transform.position) >= (navAgent.stoppingDistance + 0.5))
        {
            yield return null;
        }
        //grab item
        GameObject grabbedItem;
        if (pickUpPoint.GetItemCount() > 0)
        {
            grabbedItem = pickUpPoint.GetItem();
            Debug.Log(name + " picked up an item");
        }
        else
        {
            Debug.Log("Pick up point has no items for: " + name);
            yield break;
        }
        
        //go to put down point
        navAgent.Move(putDownPoint.gameObject.transform.position);

        //don't continue until reached
        while (Vector3.Distance(putDownPoint.gameObject.transform.position, gameObject.transform.position) >= (navAgent.stoppingDistance + 0.5))
        {
            yield return null;
        }





    }
}
