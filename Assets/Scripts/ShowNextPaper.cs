using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNextPaper : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // SpriteRenderer bileþeni
    private Color originalColor; // Orijinal rengi
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); // Hafif sarý renk
    [SerializeField] GameObject otherPaper;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        otherPaper.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlightColor;
            otherPaper.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
            otherPaper.SetActive(false);
        }
    }
}
