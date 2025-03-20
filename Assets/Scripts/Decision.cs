using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{
    public TrackingFinances trackingFinances;
    Product desired_product;
    void Assign(){
        desired_product = trackingFinances.products[0];
    }
    
    public bool ShouldBuyProduct(float money, float price, float greediness)
    {
        Assign();
        float affordabilityFactor = money / desired_product.Price;
        float buyChance = (1 - greediness) * affordabilityFactor * Random.Range(0.5f, 1.5f);
        if (buyChance > 1.0f) {
            return true;
        }
        else{
            return false;
        }
    }


    void Start()
    {
    }


    void Update()
    {
        
    }
}
