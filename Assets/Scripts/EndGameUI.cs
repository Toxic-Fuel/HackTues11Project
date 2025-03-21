using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    public Graph graphPrice;
    public Graph graphSold;
    public WeeklyFinances wf;
    public int maxWeeks = 7;
    public SetAveragePriceText avgPriceScript;

    WeeklyFinances weeklyFinances;
    int currentItem = 0;
    bool Done = false;
    private void Start()
    {
        weeklyFinances = wf;
    }
    public void Update()
    {
        if(weeklyFinances.DaysPassed >= 49 && Done==false)
        {
            EndGame();
            Done = true;
        }
    }
    public void EndGame()
    {
        
        float[,] History = new float[maxWeeks, 2];
        float[] HistoryForProduct0 = new float[maxWeeks];
        
        //GameObject newInstance = Instantiate(EndGameCanvas, Vector3.zero, Quaternion.identity);
        for(int i=0; i<maxWeeks; i++)
        {

            HistoryForProduct0[i] = weeklyFinances.AveragePrices[i, 0];
            //History[i,0] = wf.AveragePrices[i, 0];
            //History[i, 1] = statistics.getStatisticAvgSells(i);

        }
        Debug.Log("History length"+History.Length);
        avgPriceScript.SetTexts();
        ShowPriceGraph(HistoryForProduct0);
        ShowSalesGraph(History);
        
    }

    private void ShowPriceGraph(float[] priceHistory)
    {
        for (int i = 0; i < maxWeeks; i++)
        {
            graphPrice.AddDataPoint(i, priceHistory[i]);
        }
        
    }

    private void ShowSalesGraph(float[,] salesHistory)
    {
        for (int i = 0; i < maxWeeks / 7; i++)
        {
            graphSold.AddDataPoint(i, salesHistory[i, 1]);
        }
    }
}
