using UnityEngine;

public class ChainController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f);
    [SerializeField] private float pullDistance = 2f;
    [SerializeField] private float pullSpeed = 5f; 

    private Vector3 initialPosition; 
    private bool isPulled = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        initialPosition = transform.position;
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
