using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMoneyWinOrLose : MonoBehaviour
{
    [SerializeField] private GameObject x; // Para miktarýný kontrol edeceðimiz obje
    [SerializeField] private int enoughGold = 19; // Kazanmak için gerekli minimum altýn miktarý

    private int currentGold; // x objesindeki para miktarý

    void Start()
    {
        if (x == null)
        {
            Debug.LogError("Para miktarýný kontrol etmek için x objesi atanmadý!");
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
                Debug.LogError("x objesinde GoldManager bileþeni bulunamadý!");
                return;
            }

            // Altýn miktarýný kontrol et
            if (currentGold < enoughGold)
            {
                Debug.Log("Kaybettin!");
            }
        }
    }
}