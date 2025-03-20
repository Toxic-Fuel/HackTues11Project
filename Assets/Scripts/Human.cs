using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public WALLETSCRIPT.Wallet wallet;
    float greediness;
    void Start()
    {
        wallet.AddMoney(Random.Range(1000, 5000));
        greediness = Random.Range(0.1f, 0.5f);
    }
}
