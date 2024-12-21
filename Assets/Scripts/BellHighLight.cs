using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellHighLight : MonoBehaviour
{
    [SerializeField] public GameObject bellIcon;
    [SerializeField] private EntitiesMovement entitiesMovement; 
    private SpriteRenderer spriteRenderer;
    private Color originalColor; 
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f);

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
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

    void OnMouseDown()
    {
        if (entitiesMovement != null && !entitiesMovement.IsMoving())
        {
            entitiesMovement.TriggerMovement();
        }
        else if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement referans� bulunamad�.");
        }
    }
}