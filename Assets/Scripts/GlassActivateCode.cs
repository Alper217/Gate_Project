using UnityEngine;

public class GlassActivateCode : MonoBehaviour
{
    [SerializeField] public GameObject onBoardObject;
    [SerializeField] public GameObject targetObject;
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
            Debug.LogError("SpriteRenderer bile�eni bulunamad�.");
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
    if (onBoardObject != null && targetObject != null)
    {
        onBoardObject.SetActive(false);
        targetObject.SetActive(true);
    }
}
}
