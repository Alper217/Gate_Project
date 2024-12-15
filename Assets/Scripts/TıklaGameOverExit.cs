using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName; // Hedef sahnenin adı (Inspector'dan ayarlanabilir)

    // Nesneye tıklanınca çalışır
    private void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(sceneName)) // Sahne adı boş değilse
        {
            SceneManager.LoadScene(sceneName); // Belirtilen sahneye geç
        }
        else
        {
            Debug.LogError("Sahne adı belirtilmedi! Lütfen Inspector'da sahne adını girin.");
        }
    }
}
