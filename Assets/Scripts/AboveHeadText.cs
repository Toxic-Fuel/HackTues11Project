using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AboveHeadText : MonoBehaviour
{
    public TMP_Text text;
    
    public void UpdateText(string newText)
    {
        text.text = newText;
    }
    private void Update()
    {
        transform.LookAt(Camera.main.GetComponent<Transform>().position);
    }
}
