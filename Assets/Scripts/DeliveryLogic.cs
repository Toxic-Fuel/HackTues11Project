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
        PickUpPlace = availablePickUpPoints[Random.Range(0, availablePickUpPoints.Count)];
        PutDownPlace = availableDropOffPoints[Random.Range(0, availableDropOffPoints.Count)];




    }
}
