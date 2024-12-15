using UnityEngine;

public class ChainController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Zincirin SpriteRenderer bileþeni
    private Color originalColor; // Orijinal rengi
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); // Hafif sarý renk
    [SerializeField] private float pullDistance = 2f; // Zincirin yukarý çekileceði mesafe
    [SerializeField] private float pullSpeed = 5f; // Zincirin çekilme ve geri dönme hýzý

    private Vector3 initialPosition; // Zincirin baþlangýç pozisyonu
    private bool isPulled = false; // Zincir þu anda çekiliyor mu?

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
            Debug.LogError("SpriteRenderer bileþeni bulunamadý. Lütfen bu script'i bir SpriteRenderer içeren zincir nesnesine ekleyin.");
        }

        // Zincirin baþlangýç pozisyonunu kaydet
        initialPosition = transform.position;
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
        // Zinciri çekmeye baþlat
        if (!isPulled)
        {
            StartCoroutine(PullChain());
        }
    }

    System.Collections.IEnumerator PullChain()
    {
        isPulled = true;

        // Zinciri yukarý çek
        Vector3 targetPosition = initialPosition + new Vector3(0, pullDistance, 0);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, pullSpeed * Time.deltaTime);
            yield return null;
        }

        // Zinciri geri býrak
        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, pullSpeed * Time.deltaTime);
            yield return null;
        }

        isPulled = false;
    }
}
