using System;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PriceCheckController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI sacText;
    [SerializeField] private GameObject animatedObject;
    private ButtonSettings buttonSettings;
    private EntitiesMovement entitiesMovement;
    private int totalAmount;

    void Start()
    {
        entitiesMovement = FindObjectOfType<EntitiesMovement>();
        buttonSettings = FindObjectOfType<ButtonSettings>();
        if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement script not found in the scene.");
        }
    }

    public void PriceOkey()
    {
        int price = Convert.ToInt32(textMeshProUGUI.text); 
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

        // GameObject'i animasyon için çaðýr
        if (animatedObject != null)
        {
            StartCoroutine(AnimateObject(animatedObject));
        }
        else
        {
            Debug.LogError("Animated Object is not assigned in the Inspector.");
        }
    }

    private IEnumerator AnimateObject(GameObject obj)
    {
        Vector3 originalPosition = obj.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, -2, 0);

        float elapsedTime = 0f;
        float duration = 0.5f; // Animasyon süresi

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = originalPosition;
    }
}
