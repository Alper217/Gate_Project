using System;
using TMPro;
using UnityEngine;

public class PriceCheckController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI sacText;
    ButtonSettings buttonSettings;
    private EntitiesMovement entitiesMovement;
    int totalAmount;

    void Start()
    {
        // EntitiesMovement scriptini sahnede buluyoruz
        entitiesMovement = FindObjectOfType<EntitiesMovement>();
        buttonSettings = FindObjectOfType<ButtonSettings>();
        if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement script not found in the scene.");
        }
    }

    public void PriceOkey()
    {
        int price = Convert.ToInt32(textMeshProUGUI.text); // textMeshProUGUI.text'ini int'e çeviriyoruz
        if (entitiesMovement.objects == null || entitiesMovement.objects.Length == 0)
        {
            Debug.LogError("Objects array is empty or not assigned.");
            return;
        }

        GameObject currentObject = entitiesMovement.objects[entitiesMovement.currentObjectIndex];

        if (currentObject == null)
        {
            Debug.LogError("Current object is null.");
            return;
        }

        CharacterProperties characterProperties = currentObject.GetComponent<CharacterProperties>();

        if (characterProperties != null && entitiesMovement.checkPrice == true)
        {
            if (price <= characterProperties.Price)
            {
                Debug.Log("Bu karakter geçebilir");
                entitiesMovement.SetPriceCheckResult(true);
                int amount = Convert.ToInt32(textMeshProUGUI.text);
                totalAmount += amount;
                sacText.text = totalAmount.ToString();
                Debug.Log("Para eklendi");
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
