using UnityEngine;
using TMPro;

public class SacInfo : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private SpriteRenderer triangleRenderer;
    private Color originalColor;
    public AudioSource src;
    [SerializeField] private AudioClip sfx1;

    void Start()
    {
        triangleRenderer = GetComponent<SpriteRenderer>();
        originalColor = triangleRenderer.color;

        if (textMeshPro != null)
        {
            textMeshPro.enabled = false;
        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            OnMouseOverTriangle();
        }
        else
        {
            OnMouseExitTriangle();
        }
    }

    void OnMouseOverTriangle()
    {
        triangleRenderer.color = Color.yellow;
        if (textMeshPro != null)
        {
            textMeshPro.enabled = true;
        }
    }

    void OnMouseExitTriangle()
    {
        triangleRenderer.color = originalColor;
        if (textMeshPro != null)
        {
            textMeshPro.enabled = false;
        }
        src.clip = sfx1;
        src.Play();
    }
}
