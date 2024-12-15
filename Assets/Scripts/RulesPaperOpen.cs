using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesPaperOpen : MonoBehaviour
{
    [SerializeField] public GameObject ruleIcon;
    [SerializeField] private GameObject ruleObject; // A��lacak kitap objesi
    private SpriteRenderer spriteRenderer; // SpriteRenderer bile�eni
    private Color originalColor; // Orijinal rengi
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); // Hafif sar� renk

    void Start()
    {
        // SpriteRenderer bile�enini al ve orijinal rengi sakla
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer bile�eni bulunamad�. L�tfen bu script'i bir SpriteRenderer i�eren nesneye ekleyin.");
        }
    }

    void OnMouseEnter()
    {
        // Fare �zerine geldi�inde rengi de�i�tir
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlightColor;
        }
    }

    void OnMouseExit()
    {
        // Fare nesneden ayr�ld���nda orijinal rengine d�n
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    void OnMouseDown()
    {
        // Fare ile t�klan�rsa kitap objesini aktif et
        if (ruleObject != null)
        {
            ruleObject.SetActive(true);
            ruleIcon.SetActive(false);
        }
        else
        {
            Debug.LogError("BookObject atanmad�. L�tfen Inspector'dan tan�mlay�n.");
        }
    }
}
