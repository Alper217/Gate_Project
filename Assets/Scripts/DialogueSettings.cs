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
            int index = i;
            Button button = buttons[i].GetComponent<Button>();
            button.onClick.AddListener(() => OnButtonClicked(index));
        }
    }
    void OnButtonClicked(int index)
    {
        foreach (var dialogBox in dialogBoxes)
        {
            dialogBox.SetActive(false);
        }
        if (index >= 0 && index < dialogBoxes.Length)
        {
            dialogBoxes[index].SetActive(true);
        }
    }
}
