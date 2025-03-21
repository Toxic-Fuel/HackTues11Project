using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;


public class WeeklyFinances : MonoBehaviour
{
    public TrackingFinances tf;
    public List<TrackingFinances> TrackerRecord;
    public int DaysPassed = 1;
    public float dayLengthSeconds = 10f;
    public float inflationPercent = 0.1f;
    public Statistics statistics;
    public WareHouse wareHouse;
    public int dailyAmount = 3;
    Stopwatch daySW = new Stopwatch();
    float[] sumPrice;
    float[] sumNumPurchases;
    float[] sumLastPurchases;
   

    void Start()
    {
        daySW.Start();
        
    }
    public float Reprice(float price, float numPurchasesLastWeek, float numPurchases2Weeksago, float percentInflationPerPurchase)
    {
        float inflation = (numPurchasesLastWeek- numPurchases2Weeksago) * percentInflationPerPurchase;
        price *= 1 + (inflation);
        return price;
    }
    public List<TrackingFinances> GetListOfRecords()
    {
        return TrackerRecord;
    }

    // Update is called once per frame
    void Update()
    {
        if(daySW.ElapsedMilliseconds >= dayLengthSeconds * 1000)
        {
            wareHouse.Reload(dailyAmount, tf);
            TrackingFinances newTf = tf.CloneViaFakeSerialization();
            TrackerRecord.Add(newTf);
            if (DaysPassed % 7 == 0 )
            {
                for(int i=0; i<tf.products.Count; i++)
                {
                    if (sumPrice == null)
                    {
                        sumPrice = new float[tf.products.Count];
                    }
                    sumPrice[i] = statistics.getStatisticAvgPrice(i);

                    if (sumNumPurchases == null)
                    {
                        sumNumPurchases = new float[tf.products.Count];
                        sumLastPurchases = new float[tf.products.Count];
                    }

                    if (DaysPassed >= 7)
                    {
                        sumLastPurchases = sumNumPurchases;
                    }
                    sumNumPurchases[i] = statistics.getStatisticAvgSells(i);
                    
                }
                
                if(DaysPassed >= 14)
                {
                    for (int i = 0; i < tf.products.Count; i++)
                    {
                        
                        tf.ModifyProduct(i, Reprice(tf.products[i].Price, sumNumPurchases[i]/7, sumLastPurchases[i]/7,inflationPercent), tf.products[i].NumberOfTimesSold);
                        UnityEngine.Debug.Log("New price for "+ tf.products[i].ProductName + " is "+ tf.products[i].Price);
                    }
                }
                
            }
            
            
            
            DaysPassed++;
            daySW.Restart();
        }
    }
}
