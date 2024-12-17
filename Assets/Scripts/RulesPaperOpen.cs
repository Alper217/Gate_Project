using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesPaperOpen : MonoBehaviour
{
    [SerializeField] public GameObject ruleIcon;
    [SerializeField] private GameObject ruleObject;
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

    void OnMouseDown()
    {
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
