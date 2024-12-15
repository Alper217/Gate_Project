using System.Collections;
using System.Collections.Generic;
using TMPro; // TextMeshPro kullanýmý için
using UnityEngine;

public class BellHighLight : MonoBehaviour
{
    [SerializeField] public GameObject bellIcon;
    [SerializeField] private EntitiesMovement entitiesMovement; // EntitiesMovement referansý
    [SerializeField] private TMP_Text dialogueText; // Diyalog metnini gösterecek TextMeshPro alaný
    [SerializeField] private string[] dialogues; // Her karaktere özel diyaloglar
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

        // EntitiesMovement referansýný kontrol et
        if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement referansý atanmadý. Lütfen Inspector üzerinden atayýn.");
        }

        // Diyalog metni baþlangýçta gizli olsun
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
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
        // Fare ile týklanýldýðýnda hareketi baþlat
        if (entitiesMovement != null && !entitiesMovement.IsMoving())
        {
            ShowDialogue(); // Diyalog göster
            entitiesMovement.TriggerMovement(); // Hareketi baþlat
        }
        else if (entitiesMovement == null)
        {
            Debug.LogError("EntitiesMovement referansý bulunamadý.");
        }
    }

    void ShowDialogue()
    {
        if (dialogueText != null && dialogues.Length > entitiesMovement.currentObjectIndex)
        {
            // Diyalog metnini güncelle ve görünür yap
            dialogueText.text = dialogues[entitiesMovement.currentObjectIndex];
            dialogueText.gameObject.SetActive(true);

            // Bir süre sonra diyalogu gizlemek için Coroutine baþlat
            StartCoroutine(HideDialogueAfterSeconds(3f)); // Diyalog 3 saniye görünsün
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
