using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{
    public TrackingFinances trackingFinances;
    

   

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

    void Start() { }

    void Update() { }
}
    