using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryLogic : MonoBehaviour
{
    public StoragePoint PickUpPlace;
    public StoragePoint PutDownPlace;
    public StoragePoint ShopPlace;
    public StoragePoint LandfillPlace;
    public string currentTask;
    public float Salary = 1000f;
    public Human human;
    public TrackingFinances trackingFinances;
    public bool DebugMovement = false;
    public AboveHeadText aboveHeadText;

    NavMeshAgent navAgent;
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        ChooseTask(currentTask);
        trackingFinances = GameObject.Find("DailyFinanceTracker").GetComponent<TrackingFinances>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void ChooseTask(string task)
    {
        aboveHeadText.UpdateText(task);
        switch (task)
        {
            case "Deliver":
                Deliver();            
                break;
            case "FreeTime":
                FreeTime();
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
        if (DebugMovement) { Debug.Log(name + " Filtered"); }
        
        StartCoroutine(Transfer(PickUpPlace, PutDownPlace, false));

        

    }
    void FreeTime()
    {
        //find available storages
        List<StoragePoint> availableShopPoints = new List<StoragePoint>();
        List<StoragePoint> availableDropOffPoints = new List<StoragePoint>();
        GameObject[] ObjsContainingTagShop = GameObject.FindGameObjectsWithTag("shop");
        GameObject[] ObjsContainingTagLandfill = GameObject.FindGameObjectsWithTag("landfill");

        //check if points are taken already
        foreach (GameObject obj in ObjsContainingTagShop)
        {
            StoragePoint curStorage = obj.GetComponent<StoragePoint>();
            if (curStorage.GetWorker() == null)
            {
                availableShopPoints.Add(curStorage);
            }
        }
        foreach (GameObject obj in ObjsContainingTagLandfill)
        {
            StoragePoint curStorage = obj.GetComponent<StoragePoint>();
            if (curStorage.GetWorker() == null)
            {
                availableDropOffPoints.Add(curStorage);
            }
        }
        //assign the 2 points
        ShopPlace = availableShopPoints[Random.Range(0, availableShopPoints.Count)];
        LandfillPlace = availableDropOffPoints[Random.Range(0, availableDropOffPoints.Count)];
        if (DebugMovement)
        { Debug.Log(name + " Filtered"); }
        
        int index = -1;
        try
        {
            if(ShopPlace.items.Count > 0)
            {
                index = ShopPlace.items[Random.Range(0, ShopPlace.items.Count)].GetComponent<Item>().GetIndex();
            }
            
        }
        catch
        {
            ChooseTask("Deliver");

        }

        //decide if should buy
        if (index != -1)
        {
            if (ShouldBuyProduct(human.wallet.GetBalance(), human.greediness, trackingFinances.products[index].Price))
            {
                human.wallet.RemoveMoney(trackingFinances.products[index].Price);
                trackingFinances.ModifyProduct(index, trackingFinances.products[index].Price, trackingFinances.products[index].NumberOfTimesSold + 1);
                StartCoroutine(Transfer(ShopPlace, LandfillPlace, true));
            }
            else
            {
                ChooseTask("Deliver");
            }
        }
        
        
        else
        {
            ChooseTask("Deliver");
        }
        
    }
    //Transfer process
    private IEnumerator Transfer(StoragePoint pickUpPoint, StoragePoint putDownPoint, bool oneTimeOnly)
    {
        //go to pickup point
        navAgent.SetDestination(pickUpPoint.gameObject.transform.position);
        if (DebugMovement)
        {
            Debug.Log(name + " started moving to pickup");
        }
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
                if (DebugMovement)
                {
                    if (DebugMovement)
                    {
                        Debug.Log(name + " picked up " + grabbedItem.name);
                    }
                }
            }
            catch
            {
                if (DebugMovement)
                {
                    if (DebugMovement)
                    {
                        Debug.Log("Unsuccessful grab for " + name);
                    }
                }
                yield break;
            }
            if (DebugMovement)
            {
                Debug.Log(name + " picked up an item");
            }
        }
        else
        {
            if (DebugMovement)
            {
                if (DebugMovement)
                {
                    Debug.Log("Pick up point has no items for: " + name);
                }
            }
            if (oneTimeOnly)
            {
                ChooseTask("Deliver");
                yield break;
            }
            else
            {
                ChooseTask("FreeTime");
                yield break;
            }
                
            
        }
        
        //go to put down point
        navAgent.SetDestination(putDownPoint.gameObject.transform.position);
        

        //don't continue until reached
        while (Vector3.Distance(putDownPoint.gameObject.transform.position, gameObject.transform.position) >= (navAgent.stoppingDistance + 0.5))
        {
            yield return null;
        }
        if (DebugMovement)
        {
            if (DebugMovement)
            {
                Debug.Log(name + " reached drop point");
            }
        }
        //put down
        try 
        {
            putDownPoint.AddItem(grabbedItem.gameObject);
        }
        catch
        {
            grabbedItem.transform.parent = null;
            grabbedItem = null;
            if (DebugMovement)
            {
                Debug.Log("Bot " + name + " failed at dropping");
            }
            yield break;
        }

        //starts coroutine again - needs a chance to rest
        if (oneTimeOnly == false)
        {
            human.wallet.AddMoney(Salary);
        }
        
        if (oneTimeOnly)
        {
            ChooseTask("Deliver");
            yield break;
        }
        if(Random.Range(0, 1) > human.laziness)
        {
           StartCoroutine(Transfer(pickUpPoint, putDownPoint, false));
        }
        else
        {
           ChooseTask("FreeTime");
            yield break;
        }
        
        yield return null;

        







    }

    public bool ShouldBuyProduct(float money, float greediness, float productPrice)
    {
        //Assign();
        float affordabilityFactor = money / productPrice;
        float buyChance = (1 - greediness) * affordabilityFactor * Random.Range(0.5f, 1.5f);
        if (buyChance > 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
