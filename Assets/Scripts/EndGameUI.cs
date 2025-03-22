using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    public Graph[] graphPrice;
    public Graph graphSold;
    public WeeklyFinances wf;
    public int maxWeeks = 7;
    public SetAveragePriceText avgPriceScript;
    public GameObject background;
    public int ProductToShow = 0;
    WeeklyFinances weeklyFinances;
    public int currentItem = 0;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        float[,] History = new float[maxWeeks, 2];
        float[] HistoryForProduct0 = new float[maxWeeks];
        
        //GameObject newInstance = Instantiate(EndGameCanvas, Vector3.zero, Quaternion.identity);
        for(int i=0; i<maxWeeks; i++)
        {

            HistoryForProduct0[i] = weeklyFinances.AveragePrices[i, ProductToShow];
            //History[i,0] = wf.AveragePrices[i, 0];
            //History[i, 1] = statistics.getStatisticAvgSells(i);

        }
        Debug.Log("History length"+History.Length);
        background.SetActive(true);
        avgPriceScript.SetTexts();
        ShowPriceGraph(HistoryForProduct0);
        ShowSalesGraph(History);
        
        
    }

    private void ShowPriceGraph(float[] priceHistory)
    {
        for (int i = 0; i < maxWeeks; i++)
        {
            if(i== maxWeeks - 1)
            {
                graphPrice[currentItem].AddDataPoint(i, priceHistory[i-1]);
            }
            else
            {
                graphPrice[currentItem].AddDataPoint(i, priceHistory[i]);
            }
            
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
