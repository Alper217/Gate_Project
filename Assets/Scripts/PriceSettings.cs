using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceSettings : MonoBehaviour
{
    [SerializeField] TextMeshPro _textMeshPro;
    int price = 0;
     void DecreasePrice()
    {
        if (Input.GetButtonDown("Decrease"))
        {
            price = -1;
            if (price < 0) price = 0;
           _textMeshPro.text = price.ToString();
        }
    }
    void IncreasePrice()
    {
        if (Input.GetButtonDown("Increase"))
        {
            price =+ 1;
            _textMeshPro.text = price.ToString();
        }
    }
}
