using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Graph : MonoBehaviour
{
    public UILineRenderer lineRenderer;
    public RectTransform graphPanel;
    public float maxPrice;
    int maxDays = 7;

    private List<Vector2> graphPoints = new List<Vector2>();

    public void AddDataPoint(int day, float revenue)
    {
        float panelWidth = graphPanel.rect.width;
        float panelHeight = graphPanel.rect.height;

        float x = (day) / (float)maxDays * panelWidth;
        float y = revenue / maxPrice * panelHeight;

        x -= panelWidth / 2f;
        y -= panelHeight / 2f;
        graphPoints.Add(new Vector2(x, y));
        Debug.Log("Will enter " + revenue + " on day " + day);
        UpdateGraph();
    }

    void UpdateGraph()
    {
        lineRenderer.Points = graphPoints.ToArray();
        lineRenderer.SetAllDirty();
    }
}
