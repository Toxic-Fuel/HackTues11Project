using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public WALLETSCRIPT wallet;
    public float greediness;
    public float laziness;
    void Start()
    {
        wallet.AddMoney(Random.Range(1000, 5000));
        greediness = Random.Range(0.1f, 0.5f);
        laziness = Random.Range(0.1f, 0.5f);
    }
}
