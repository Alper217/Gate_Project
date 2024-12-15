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
            int index = i;  // Buton index'ini sabitlemek için
            Button button = buttons[i].GetComponent<Button>();
            button.onClick.AddListener(() => OnButtonClicked(index));  // Buton týklandýðýnda OnButtonClicked metodu çaðrýlacak
        }
    }

    // Butona týklanýnca çaðrýlacak metod
    void OnButtonClicked(int index)
    {
        // Önce tüm dialogBox'larý kapatalým
        foreach (var dialogBox in dialogBoxes)
        {
            dialogBox.SetActive(false);
        }

        // Ardýndan, týklanan butonun index'ine karþýlýk gelen dialogBox'ý açalým
        if (index >= 0 && index < dialogBoxes.Length)
        {
            dialogBoxes[index].SetActive(true);
        }
    }
}
