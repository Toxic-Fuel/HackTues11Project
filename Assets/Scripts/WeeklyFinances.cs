using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class WeeklyFinances : MonoBehaviour
{
    public TrackingFinances tf;
    public List<TrackingFinances> TrackerRecord;
    public float[,] AveragePrices;
    public int DaysPassed = 1;
    public float dayLengthSeconds = 10f;
    public float inflationPercent = 0.1f;
    public Statistics statistics;
    public WareHouse wareHouse;
    public int dailyAmount = 3;
    public int maxWeeks = 7;
    public TMP_Text counterText;
    Stopwatch daySW = new Stopwatch();
    float[] sumPrice;
    float[] sumNumPurchases;
    float[] sumLastPurchases;
   

    void Start()
    {
        daySW.Start();
        wareHouse.Reload(dailyAmount, tf);
        AveragePrices = new float[maxWeeks, 3];
        counterText.text = "Current Day: " + DaysPassed.ToString();

    }
    public float Reprice(float price, float numPurchasesLastWeek, float numPurchases2Weeksago, float percentInflationPerPurchase)
    {
        float inflation = (numPurchasesLastWeek- numPurchases2Weeksago) * percentInflationPerPurchase;
        float fprice = price * (1 + inflation);
        return fprice;
    }
    public List<TrackingFinances> GetListOfRecords()
    {
        return TrackerRecord;
    }

    // Update is called once per frame
    void Update()
    {
        if(daySW.ElapsedMilliseconds >= dayLengthSeconds * 1000 && DaysPassed <= 49)
        {
            counterText.text = "Current Day: " + DaysPassed.ToString();
            TrackingFinances newTf = tf.CloneViaFakeSerialization();
            TrackerRecord.Add(newTf);
            if (DaysPassed % 7 == 0 )
            {
                if (sumNumPurchases == null)
                {
                    sumNumPurchases = new float[tf.products.Count];
                    sumLastPurchases = new float[tf.products.Count];
                }
                wareHouse.Reload(dailyAmount, tf);
                if (DaysPassed >= 14)
                {
                    for (int i = 0; i < tf.products.Count; i++)
                    {
                        sumLastPurchases[i] = sumNumPurchases[i];
                    }
                }
                for (int i=0; i<tf.products.Count; i++)
                {
                    if (sumPrice == null)
                    {
                        sumPrice = new float[tf.products.Count];
                    }
                    sumPrice[i] = statistics.getStatisticAvgPrice(i);
                    try
                    {
                        AveragePrices[(DaysPassed / 7)-1, i] = sumPrice[i];
                    }
                    catch
                    {
                        UnityEngine.Debug.LogError("Index: " + i + " is out of array");
                    }
                    
                    UnityEngine.Debug.Log("The price for " + i + " is " + sumPrice[i]);
                    

                    
                    sumNumPurchases[i] = statistics.getStatisticAvgSells(i);
                    UnityEngine.Debug.Log("Old puraches " + sumLastPurchases[0] + "New purchases " + sumNumPurchases[0]);


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
