using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GraphHUD : MonoBehaviour
{
    public UILineRenderer lineRenderer;
    public RectTransform graphPanel;
    public float maxRevenue = 5000f;
    public int maxDays = 49;

    private List<Vector2> graphPoints = new List<Vector2>();

    public void AddDataPoint(int day, float revenue)
    {
        float panelWidth = graphPanel.rect.width;
        float panelHeight = graphPanel.rect.height;
        Debug.Log(panelWidth);
        Debug.Log(panelHeight);
        Debug.Log(maxRevenue);

        float x = ((day - 1) / (float)maxDays) * panelWidth;
        float y = (revenue / maxRevenue) * panelHeight;

        x -= panelWidth / 2f;
        y -= panelHeight / 2f;
        Debug.Log(y);
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
        AddDataPoint(11, 5000);
    }
}
