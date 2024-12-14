using UnityEngine;

public class BookOpen : MonoBehaviour
{
    [SerializeField] public GameObject bookIcon;
    [SerializeField] private GameObject bookObject; // Açýlacak kitap objesi
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
        // Fare ile týklanýrsa kitap objesini aktif et
        if (bookObject != null)
        {
            bookObject.SetActive(true);
            bookIcon.SetActive(false);
        }
        else
        {
            Debug.LogError("BookObject atanmadý. Lütfen Inspector'dan tanýmlayýn.");
        }
    }
}
