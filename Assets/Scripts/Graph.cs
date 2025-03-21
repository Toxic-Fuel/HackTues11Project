using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GraphHUD : MonoBehaviour
{
    public UILineRenderer lineRenderer;
    public RectTransform graphPanel;
    public float maxRevenue = 1000f;
    public int maxDays = 30;

    private List<Vector2> graphPoints = new List<Vector2>();

    public void AddDataPoint(int day, float revenue)
    {
        float x = day / (float)maxDays * graphPanel.rect.width - graphPanel.rect.width / 2;
        float y = revenue / maxRevenue * graphPanel.rect.height - graphPanel.rect.height / 2;
        graphPoints.Add(new Vector2(x, y));
        UpdateGraph();
    }

    void UpdateGraph()
    {
        lineRenderer.Points = graphPoints.ToArray();
        lineRenderer.SetAllDirty();
    }

    void Start()
    {
        AddDataPoint(1, 200);
        AddDataPoint(2, 500);
        AddDataPoint(3, 750);
        AddDataPoint(4, 400);
        AddDataPoint(5, 900);
        AddDataPoint(6, 600);
        AddDataPoint(7, 700);
        AddDataPoint(8, 600);
        AddDataPoint(9, 300);
        AddDataPoint(10, 200);
    }
}
