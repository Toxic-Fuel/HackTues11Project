using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{
    Product desired_product = products[0];
    
    public bool ShouldBuyProduct(WALLETSCRIPT.Wallet wallet, desired_product.Price, float greediness)
    {
        float affordabilityFactor = wallet.GetBalance() /  desired_product.Price;
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
