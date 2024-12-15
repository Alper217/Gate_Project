using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellHighLight : MonoBehaviour
{
    [SerializeField] public GameObject bellIcon;
    [SerializeField] private EntitiesMovement entitiesMovement; // EntitiesMovement referansý
    private SpriteRenderer spriteRenderer; // SpriteRenderer bileþeni
    private Color originalColor; // Orijinal rengi
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); // Hafif sarý renk

    void Start()
    {
        // SpriteRenderer bileþenini al ve orijinal rengi sakla
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer bileþeni bulunamadý. Lütfen bu script'i bir SpriteRenderer içeren nesneye ekleyin.");
        }

        // EntitiesMovement referansýný kontrol et
        if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement referansý atanmadý. Lütfen Inspector üzerinden atayýn.");
        }
    }

    void OnMouseEnter()
    {
        // Fare üzerine geldiðinde rengi deðiþtir
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlightColor;
        }
    }

    void OnMouseExit()
    {
        // Fare nesneden ayrýldýðýnda orijinal rengine dön
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    void OnMouseDown()
    {
        // Fare ile týklanýldýðýnda hareketi baþlat
        if (entitiesMovement != null && !entitiesMovement.IsMoving())
        {
            entitiesMovement.TriggerMovement();
        }
        else if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement referansý bulunamadý.");
        }
    }
}
