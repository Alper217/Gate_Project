using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceCheckController : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI textMeshProUGUI;
    EntitiesMovement entitiesMovement; // Referans de�i�keni

    void Start()
    {
       
        // EntitiesMovement scriptini sahnede buluyoruz
        entitiesMovement = FindObjectOfType<EntitiesMovement>();

        if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement script not found in the scene.");
        }
    }

    public void PriceOkey()
    {
        int price = Convert.ToInt32(textMeshProUGUI.text);
        // Objelerin dizisinin do�ru oldu�una emin olun
        if (entitiesMovement.objects == null || entitiesMovement.objects.Length == 0)
        {
            Debug.LogError("Objects array is empty or not assigned.");
            return;
        }

        // �u anki objeye eri�iyoruz
        GameObject currentObject = entitiesMovement.objects[entitiesMovement.currentObjectIndex];

        if (currentObject == null)
        {
            Debug.LogError("Current object is null.");
            return;
        }

        // CharacterProperties scriptine eri�iyoruz
        CharacterProperties characterProperties = currentObject.GetComponent<CharacterProperties>();

        if (characterProperties != null && entitiesMovement.checkPrice == true)
        {
            int objectPrice = characterProperties.Price;
            if (price <= characterProperties.Price)
            {
                Debug.Log("Bu karakter ge�ebilir");
            }
            else
                Debug.Log("Bu karakter ge�emez");
            Debug.Log("Current Object Price: " + objectPrice);
            entitiesMovement.checkbutton.SetActive(false);
        }
        else
        {
            Debug.LogError("CharacterProperties script not found on the object.");
        }
    }
}
