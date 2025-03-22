using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductSwitcher : MonoBehaviour
{
   
    public TMP_Text text;
    public TrackingFinances tf;
    [SerializeField]
    public EndGameUI endGameUi;
    public GameObject[] graphs;
    int currentGraph = 0;
    bool[] gotToBefore = new bool[3];
    private void Start()
    {
        for(int i = 1; i < gotToBefore.Length; i++)
        {
            gotToBefore[i] = false;
        }
        gotToBefore[0] = true;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentGraph < graphs.Length-1)
            {
                NextProduct();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentGraph > 0)
            {
                PrevProduct();
            }
            
        }
    }
    public void NextProduct()
    {
        currentGraph++;
        for(int i = 0; i < graphs.Length; i++)
        {
            if(i != currentGraph)
            {
                graphs[i].SetActive(false);
            }
            else
            {
                graphs[i].SetActive(true);
            }
        }
        endGameUi.currentItem++;
        
        endGameUi.ProductToShow++;
        if (gotToBefore[currentGraph] == false)
        {
            endGameUi.EndGame();
            gotToBefore[currentGraph] = true;
        }
        
        text.text = tf.products[endGameUi.ProductToShow].ProductName + " Price";
    }
    public void PrevProduct()
    {
        currentGraph--;
        for (int i = 0; i < graphs.Length; i++)
        {
            if (i != currentGraph)
            {
                graphs[i].SetActive(false);
            }
            else
            {
                graphs[i].SetActive(true);
            }
        }
        endGameUi.currentItem--;
        endGameUi.ProductToShow--;
        if (gotToBefore[currentGraph] == false)
        {
            endGameUi.EndGame();
            gotToBefore[currentGraph] = true;
        };
        text.text = tf.products[endGameUi.ProductToShow].ProductName + " Price";
    }
}
