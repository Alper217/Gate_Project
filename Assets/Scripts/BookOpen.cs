using UnityEngine;

public class BookOpen : MonoBehaviour
{
    [SerializeField] public GameObject bookIcon;
    [SerializeField] private GameObject bookObject;
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
        if (bookObject != null)
        {
            bookObject.SetActive(true);
            bookIcon.SetActive(false);
        }
        else
        {
            Debug.LogError("BookObject atanmad�. L�tfen Inspector'dan tan�mlay�n.");
        }
    }
}
