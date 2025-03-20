using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryLogic : MonoBehaviour
{
    public StoragePoint PickUpPlace;
    public StoragePoint PutDownPlace;
    public string currentTask;
    public float Salary = 1000f;
    public Human human;

    NavMeshAgent navAgent;
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        ChooseTask(currentTask);
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
                Deliver();
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
        GameObject[] ObjsContainingTagDropOff = GameObject.FindGameObjectsWithTag("dropoff");
        
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
        Debug.Log(name + " Filtered");
        StartCoroutine(Transfer(PickUpPlace, PutDownPlace));

        

    }
    void FreeTime()
    {

    }
    //Transfer process
    private IEnumerator Transfer(StoragePoint pickUpPoint, StoragePoint putDownPoint)
    {
        //go to pickup point
        navAgent.SetDestination(pickUpPoint.gameObject.transform.position);
        Debug.Log(name + " started moving to pickup");
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
            try
            {
                pickUpPoint.RemoveItem(grabbedItem);
                grabbedItem.transform.parent = transform;
                grabbedItem.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                Debug.Log(name+" picked up "+grabbedItem.name);
            }
            catch
            {
                Debug.Log("Unsuccessful grab for " + name);
                yield break;
            }
            Debug.Log(name + " picked up an item");
        }
        else
        {
            Debug.Log("Pick up point has no items for: " + name);
            yield break;
        }
        
        //go to put down point
        navAgent.SetDestination(putDownPoint.gameObject.transform.position);
        

        //don't continue until reached
        while (Vector3.Distance(putDownPoint.gameObject.transform.position, gameObject.transform.position) >= (navAgent.stoppingDistance + 0.5))
        {
            yield return null;
        }
        Debug.Log(name+" reached drop point");
        //put down
        try 
        {
            putDownPoint.AddItem(grabbedItem.gameObject);
        }
        catch
        {
            grabbedItem.transform.parent = null;
            grabbedItem = null;
            Debug.Log("Bot " + name + " failed at dropping");
            yield break;
        }

        //starts coroutine again - needs a chance to rest
        human.wallet.AddMoney(Salary);
        //if(Random.Range(0, 1) > human.laziness)
        //{
           StartCoroutine(Transfer(pickUpPoint, putDownPoint));
        //}
        //else
        //{
        //    ChooseTask("FreeTime");
        //}
        
        yield return null;

        







    }
}
