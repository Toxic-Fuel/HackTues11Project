using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Graph : MonoBehaviour
{
    public UILineRenderer lineRenderer;
    public RectTransform graphPanel;
    public float maxPrice;
    public int maxDays;

    private List<Vector2> graphPoints = new List<Vector2>();

    public void AddDataPoint(int day, float revenue)
    {
        float panelWidth = graphPanel.rect.width;
        float panelHeight = graphPanel.rect.height;

        float x = (day - 1) / (float)maxDays * panelWidth;
        float y = revenue / maxPrice * panelHeight;

        x -= panelWidth / 2f;
        y -= panelHeight / 2f;
        graphPoints.Add(new Vector2(x, y));
        UpdateGraph();
    }

    void UpdateGraph()
    {
        lineRenderer.Points = graphPoints.ToArray();
        lineRenderer.SetAllDirty();
    }
}
