using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    Graph graph;

    public void EndGame(List<float> priceHistory, List<int> salesHistory)
    {
        GameObject EndGameCanvas = Resources.Load<GameObject>("Canvas");
        GameObject newInstance = Instantiate(EndGameCanvas, Vector3.zero, Quaternion.identity);


        ShowPriceGraph(priceHistory);
        ShowSalesGraph(salesHistory);
    }

    private void ShowPriceGraph(List<float> priceHistory)
    {
        for (int i = 0; i < priceHistory.Count; i++)
        {
            graph.AddDataPoint(i, priceHistory[i]);
        }
    }

    private void ShowSalesGraph(List<int> salesHistory)
    {
        for (int i = 0; i < salesHistory.Count; i++)
        {
            graph.AddDataPoint(i, salesHistory[i]);
        }
    }
}
