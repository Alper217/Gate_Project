using UnityEngine;

public class GlassActivateCode : MonoBehaviour
{
    [SerializeField] public GameObject onBoardObject; // Masadaki obje
    [SerializeField] public GameObject targetObject;  // Sürüklenebilir obje
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); // Sarý renk

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer bileþeni bulunamadý.");
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
        // Týklanýnca masa objesini kapat, sürüklenebilir objeyi aç
        if (onBoardObject != null && targetObject != null)
        {
            onBoardObject.SetActive(false);
            targetObject.SetActive(true);
        }
    }
}
