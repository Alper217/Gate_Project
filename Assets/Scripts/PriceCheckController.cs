using System;
using TMPro;
using UnityEngine;

public class PriceCheckController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] TextMeshProUGUI sacText;
    ButtonSettings buttonSettings;
    private EntitiesMovement entitiesMovement; // Referans deðiþkeni

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
        // Objelerin dizisinin doðru olduðuna emin olun
        if (entitiesMovement.objects == null || entitiesMovement.objects.Length == 0)
        {
            Debug.LogError("Objects array is empty or not assigned.");
            return;
        }

        // Þu anki objeye eriþiyoruz
        GameObject currentObject = entitiesMovement.objects[entitiesMovement.currentObjectIndex];

        if (currentObject == null)
        {
            Debug.LogError("Current object is null.");
            return;
        }

        // CharacterProperties scriptine eriþiyoruz
        CharacterProperties characterProperties = currentObject.GetComponent<CharacterProperties>();

        if (characterProperties != null && entitiesMovement.checkPrice == true)
        {
            if (price <= characterProperties.Price)
            {
                Debug.Log("Bu karakter geçebilir");
                entitiesMovement.SetPriceCheckResult(true);
                int priceSac = buttonSettings.price;
                sacText.text = priceSac.ToString();
            }
            else
            {
                Debug.Log("Bu karakter geçemez");
                entitiesMovement.SetPriceCheckResult(false);
            }

            Debug.Log("Current Object Price: " + characterProperties.Price);
        }
        else
        {
            Debug.LogError("CharacterProperties script not found on the object.");
        }
    }
}