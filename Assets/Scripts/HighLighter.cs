using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLighter : MonoBehaviour
{
    [SerializeField] public GameObject Icons;
    private SpriteRenderer spriteRenderer; 
    private Color originalColor;
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); // Hafif sar� renk

    void Start()
    {
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
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlightColor;
        }
    }

    void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}
