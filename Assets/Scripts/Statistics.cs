using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public WeeklyFinances wf;
    GameObject[] foundBots;
    int week = 0;
    public float overallMoney()
    {
        float sum = 0;
        foundBots = GameObject.FindGameObjectsWithTag("Bot");
        foreach (GameObject bot in foundBots)
        {
            if (bot.GetComponent<Human>() != null)
            {
                sum += bot.GetComponent<Human>().wallet.GetBalance();
            }
        }
        return sum;
    }

    float sumPrice(int index)
    {
        float sum = 0;
        if (wf.DaysPassed % 7 == 0)
        {
            for (int i = 1; i < (wf.DaysPassed / 7); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    try
                    {
                        sum += wf.TrackerRecord[j*i].products[index].Price;
                    }
                    catch
                    {
                        Debug.LogError("Problem index: " + index + "On "+ i + " and "+j);
                    }
                    
                    
                }
            }
            return sum / 7;
        }
        return -1;
    }

    float sumSells(int index)
    {
        float sum = 0;
        if (wf.DaysPassed % 7 == 0)
        {
            for (int i = 0; i < (wf.DaysPassed / 7); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    sum += wf.TrackerRecord[j].products[index].NumberOfTimesSold;
                    
                }
            }
            return sum / wf.DaysPassed;
        }
        return -1;
    }

    public float getStatisticAvgPrice(int index)
    {
        return sumPrice(index);
    }

    public float getStatisticAvgSells(int index)
    {
        return sumSells(index);
    }
}
