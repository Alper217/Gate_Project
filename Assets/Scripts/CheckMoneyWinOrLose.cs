using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMoneyWinOrLose : MonoBehaviour
{
    [SerializeField] private GameObject x;
    [SerializeField] private int enoughGold = 19; 

    private int currentGold;

    void Start()
    {
        if (x == null)
        {
            Debug.LogError("Para miktar�n� kontrol etmek i�in x objesi atanmad�!");
        }
    }

    void Update()
    {
        if (x != null)
        {
            SacInfo sacInfo = x.GetComponent<SacInfo>();
            if (sacInfo != null)
            {
                currentGold = (int)Convert.ToUInt32(sacInfo); 
            }
            else
            {
                Debug.LogError("x objesinde GoldManager bile�eni bulunamad�!");
                return;
            }
            if (currentGold < enoughGold)
            {
                Debug.Log("Kaybettin!");
            }
        }
    }
}