using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public float fadeDuration = 2f;  // Görünmez olma süresi (saniye)

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer bileşenini al
        StartCoroutine(FadeOutCoroutine());
    }

    // Coroutine: Yavaşça şeffaflığı azaltma
    private IEnumerator FadeOutCoroutine()
    {
        float startAlpha = spriteRenderer.color.a; // Başlangıç şeffaflık değeri
        float endAlpha = 0f;  // Bitiş şeffaflık değeri (tamamen şeffaf)

        float elapsedTime = 0f; // Zamanlayıcı

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            spriteRenderer.color = newColor; // Yeni rengi uygula
            yield return null; // Bir sonraki frame'e geç
        }

        // Tamamen şeffaf olduğunda nesneyi yok et
        gameObject.SetActive(false);
    }
}
