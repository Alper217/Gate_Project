using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(sceneName)) 
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Sahne adı belirtilmedi! Lütfen Inspector'da sahne adını girin.");
        }
    }
}
