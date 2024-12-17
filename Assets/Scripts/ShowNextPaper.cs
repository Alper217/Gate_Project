using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNextPaper : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); 
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
