using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public AudioSource src;
    [SerializeField] private AudioClip sfx2;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        textComponent.text = string.Empty; // �lk ba�ta metni temizle
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;  // Yeni sat�ra ge�meden �nce metni temizle
            StartCoroutine(TypeLine());
            if (index > 0)
            {
            src.clip = sfx2;
            src.Play();
            }
        
        }
        else
        {
            gameObject.SetActive(false);  // Dialog bitti�inde nesneyi devre d��� b�rak
        }

    }
}