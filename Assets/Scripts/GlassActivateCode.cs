using UnityEngine;

public class GlassActivateCode : MonoBehaviour
{
    [SerializeField] public GameObject onBoardObject; // Masadaki obje
    [SerializeField] public GameObject targetObject;  // S�r�klenebilir obje
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); // Sar� renk

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer bile�eni bulunamad�.");
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
    // Tıklanınca masa objesini kapat, sürüklenebilir objeyi aç
    if (onBoardObject != null && targetObject != null)
    {
        onBoardObject.SetActive(false);
        targetObject.SetActive(true);
    }

    // Ses oynatma
    AudioSource audioSource = GetComponent<AudioSource>();
    if (audioSource != null)
    {
        audioSource.Play();
    }
}
}
