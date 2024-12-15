using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSettings : MonoBehaviour
{
    [SerializeField] public GameObject[] dialogBoxes; 
    [SerializeField] public GameObject[] buttons;    

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;  // Buton index'ini sabitlemek i�in
            Button button = buttons[i].GetComponent<Button>();
            button.onClick.AddListener(() => OnButtonClicked(index));  // Buton t�kland���nda OnButtonClicked metodu �a�r�lacak
        }
    }

    // Butona t�klan�nca �a�r�lacak metod
    void OnButtonClicked(int index)
    {
        // �nce t�m dialogBox'lar� kapatal�m
        foreach (var dialogBox in dialogBoxes)
        {
            dialogBox.SetActive(false);
        }

        // Ard�ndan, t�klanan butonun index'ine kar��l�k gelen dialogBox'� a�al�m
        if (index >= 0 && index < dialogBoxes.Length)
        {
            dialogBoxes[index].SetActive(true);
        }
    }
}
