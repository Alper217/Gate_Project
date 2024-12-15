using System.Collections;
using System.Collections.Generic;
using TMPro; // TextMeshPro kullan�m� i�in
using UnityEngine;

public class BellHighLight : MonoBehaviour
{
    [SerializeField] public GameObject bellIcon;
    [SerializeField] private EntitiesMovement entitiesMovement; // EntitiesMovement referans�
    [SerializeField] private TMP_Text dialogueText; // Diyalog metnini g�sterecek TextMeshPro alan�
    [SerializeField] private string[] dialogues; // Her karaktere �zel diyaloglar
    private SpriteRenderer spriteRenderer; // SpriteRenderer bile�eni
    private Color originalColor; // Orijinal rengi
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f); // Hafif sar� renk

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
            Debug.LogError("SpriteRenderer bile�eni bulunamad�. L�tfen bu script'i bir SpriteRenderer i�eren nesneye ekleyin.");
        }

        // EntitiesMovement referans�n� kontrol et
        if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement referans� atanmad�. L�tfen Inspector �zerinden atay�n.");
        }

        // Diyalog metni ba�lang��ta gizli olsun
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
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
        // Fare ile t�klan�ld���nda hareketi ba�lat
        if (entitiesMovement != null && !entitiesMovement.IsMoving())
        {
            ShowDialogue(); // Diyalog g�ster
            entitiesMovement.TriggerMovement(); // Hareketi ba�lat
        }
        else if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement referans� bulunamad�.");
        }
    }

    void ShowDialogue()
    {
        if (dialogueText != null && dialogues.Length > entitiesMovement.currentObjectIndex)
        {
            // Diyalog metnini g�ncelle ve g�r�n�r yap
            dialogueText.text = dialogues[entitiesMovement.currentObjectIndex];
            dialogueText.gameObject.SetActive(true);

            // Bir s�re sonra diyalogu gizlemek i�in Coroutine ba�lat
            StartCoroutine(HideDialogueAfterSeconds(3f)); // Diyalog 3 saniye g�r�ns�n
        }
        else
        {
            Debug.LogWarning("Diyalog metni veya diyalog listesi eksik.");
        }
    }

    IEnumerator HideDialogueAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }
    }
}
