using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class WeeklyFinances : MonoBehaviour
{
    public TrackingFinances tf;
    public List<TrackingFinances> TrackerRecord;
    public int DaysPassed = 0;
    public float dayLengthSeconds = 10f;
    public float inflationPercent = 0.1f;
    Stopwatch daySW = new Stopwatch();
   
   

    void Start()
    {
        daySW.Start();
    }
    public float Reprice(float price, int numPurchases, float percentInflationPerPurchase)
    {
        float inflation = numPurchases * percentInflationPerPurchase;
        price *= 1 + (inflation);
        return price;
    }

    // Update is called once per frame
    void Update()
    {
        if(daySW.ElapsedMilliseconds >= dayLengthSeconds * 1000)
        {
            TrackingFinances newTf = tf.CloneViaFakeSerialization();
            TrackerRecord.Add(newTf);
            for(int i=0; i < tf.products.Count; i++)
            {
                Product tempProduct = tf.products[i];
                tf.products[i] = new Product();
            }
            
            DaysPassed++;
            daySW.Restart();
        }
    }
}
