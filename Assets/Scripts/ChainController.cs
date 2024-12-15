using UnityEngine;

public class ChainController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Zincirin SpriteRenderer bile�eni
    private Color originalColor; // Orijinal rengi
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); // Hafif sar� renk
    [SerializeField] private float pullDistance = 2f; // Zincirin yukar� �ekilece�i mesafe
    [SerializeField] private float pullSpeed = 5f; // Zincirin �ekilme ve geri d�nme h�z�

    private Vector3 initialPosition; // Zincirin ba�lang�� pozisyonu
    private bool isPulled = false; // Zincir �u anda �ekiliyor mu?

    void Start()
    {
        // SpriteRenderer bile�enini al ve orijinal rengi sakla
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer bile�eni bulunamad�. L�tfen bu script'i bir SpriteRenderer i�eren zincir nesnesine ekleyin.");
        }

        // Zincirin ba�lang�� pozisyonunu kaydet
        initialPosition = transform.position;
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
        // Zinciri �ekmeye ba�lat
        if (!isPulled)
        {
            StartCoroutine(PullChain());
        }
    }

    System.Collections.IEnumerator PullChain()
    {
        isPulled = true;

        // Zinciri yukar� �ek
        Vector3 targetPosition = initialPosition + new Vector3(0, pullDistance, 0);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, pullSpeed * Time.deltaTime);
            yield return null;
        }

        // Zinciri geri b�rak
        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, pullSpeed * Time.deltaTime);
            yield return null;
        }

        isPulled = false;
    }
}
