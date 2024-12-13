using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ButtonSettings : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] GameObject _gameObject;
    int price = 0;
    public void DecreasePrice()
    {
        price -= 1;
        if (price < 0) price = 0;
        _textMeshPro.text = price.ToString();
    }
    public void IncreasePrice()
    {
        price += 1;
        _textMeshPro.text = price.ToString();
    }
    public void BookOpen()
    {
        _gameObject.SetActive(true);
    }
    public void BookClose()
    {
        _gameObject.SetActive(false);
    }
}
