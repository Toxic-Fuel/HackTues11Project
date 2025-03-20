using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repricing : MonoBehaviour
{
    public void Reprice(float price, int numPurchases, float percentInflationPerPurchase)
    {

        price *= 1 + (numPurchases * percentInflationPerPurchase);
    }
}
