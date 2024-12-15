using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntitiesMovement : MonoBehaviour
{
    [SerializeField] private ButtonSettings buttonSettings; // ButtonSettings referansı
    [SerializeField] int losePrice; // Kayıp fiyatı
    public GameObject[] objects; // Objeler listesi
    public int currentObjectIndex = 0; // Şu anki obje indeksi
    private bool isMoving = false; // Hareket kontrolü

    private Vector3 startPosition; // Başlangıç pozisyonu
    private Vector3 targetPosition; // Hedef pozisyon

    public float bobbingAmplitude = 0.2f; // Yüksekliğin genliği (yukarı-aşağı hareket miktarı)
    public float bobbingSpeed = 3f; // Yüksekliğin hızını ayarlar

    private Renderer objRenderer;
    private Color originalColor;

    public bool checkPrice = false;
    public GameObject checkbutton;
    public GameObject CikisButonu; // Yeni çıkış Butonu

    private bool priceCheckResult = false; // Fiyat kontrol sonucu

    [SerializeField] private GameObject[] dialogBoxes; // Diyalog kutuları

    private int totalMoney = 0; // Toplam para miktarı

    void Start()
    {
        // Çıkış Butonu başta inaktif
        if (CikisButonu != null)
        {
            CikisButonu.SetActive(false);
        }

        // Başlangıçta ButtonSettings referansını alıyoruz
        if (buttonSettings == null)
        {
            buttonSettings = FindObjectOfType<ButtonSettings>(); // Eğer manuel olarak atamadıysak, sahnedeki ButtonSettings nesnesini bul
        }
    }

    void Update()
    {
        // Update içine harekete dair bir işlem eklenmedi
    }

    public void TriggerMovement()
    {
        if (!isMoving && currentObjectIndex < objects.Length)
        {
            StartCoroutine(MoveObject(objects[currentObjectIndex]));
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    IEnumerator MoveObject(GameObject obj)
    {
        isMoving = true;
        checkbutton.SetActive(false); // Buton hareket bitene kadar inaktif

        objRenderer = obj.GetComponent<Renderer>();
        originalColor = objRenderer.material.color;

        objRenderer.material.color = Color.black;

        startPosition = obj.transform.position;
        targetPosition = startPosition + new Vector3(16.2f, 0, 0);

        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        // Diyalog box'ı ilk karakterin hareketi tamamlandıktan sonra aktif et
        if (dialogBoxes.Length > currentObjectIndex)
        {
            dialogBoxes[currentObjectIndex].SetActive(true); // İlgili diyalog box'ı aktif et
        }

        while (Time.time - startTime < journeyLength / 10)
        {
            float distanceCovered = (Time.time - startTime) * 10;
            float fractionOfJourney = distanceCovered / journeyLength;

            // Yatay hareket
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

            obj.transform.position = newPosition;

            // Renk geçişi
            objRenderer.material.color = Color.Lerp(Color.black, originalColor, fractionOfJourney);

            yield return null;
        }

        obj.transform.position = targetPosition;
        objRenderer.material.color = originalColor;

        checkPrice = true;
        checkbutton.SetActive(true); // Buton hareket bitince aktif
        CikisButonu.SetActive(true); // Çıkış Butonunu aktif et

        // Kullanıcı butona basana kadar bekle
        yield return new WaitUntil(() => !checkPrice);

        // Önceki diyalog kutusunu inaktif yap
        if (dialogBoxes.Length > currentObjectIndex)
        {
            dialogBoxes[currentObjectIndex].SetActive(false); // Önceki diyalog box'ını inaktif et
        }

        CikisButonu.SetActive(false); // Karakter hareket ederken çıkış butonunu inaktif yap

        // Fiyat kontrol sonucuna göre hareket et
        if (priceCheckResult)
        {
            // Fiyat kontrolü olumluysa, ButtonSettings'teki price değerini kullanarak totalMoney'yi artırıyoruz
            totalMoney += buttonSettings.price; // price değişkenini kullan
            Debug.Log("TotalMoney increased: " + totalMoney); // Debug logu ekledik
            targetPosition = targetPosition + new Vector3(16.8f, 0, 0);
            yield return StartCoroutine(MoveObjectForward(obj));
        }
        else
        {
            targetPosition = startPosition;
            yield return StartCoroutine(MoveObjectBack(obj));
        }

        // Sonraki objeye geçiş
        currentObjectIndex++;
        if (currentObjectIndex < objects.Length)
        {
            isMoving = false; // Sonraki hareket için tekrar tıklamayı bekle
            CikisButonu.SetActive(true); // Sıradaki karakter geldiğinde buton tekrar aktif
        }
        else
        {
            isMoving = false;
            CheckGameResult(); // Tüm karakterler hareket ettikten sonra oyunu kontrol et
        }
    }

    IEnumerator MoveObjectBack(GameObject obj)
    {
        Vector3 backStartPosition = obj.transform.position;
        float journeyLength = Vector3.Distance(backStartPosition, startPosition);
        float startTime = Time.time;

        while (Time.time - startTime < journeyLength / 10)
        {
            float distanceCovered = (Time.time - startTime) * 10;
            float fractionOfJourney = distanceCovered / journeyLength;

            Vector3 newPosition = Vector3.Lerp(backStartPosition, startPosition, fractionOfJourney);

            // Hareket ederken bobbing efekti uygulanır
            float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            newPosition.y += bobbingOffset;

            obj.transform.position = newPosition;

            yield return null;
        }

        obj.transform.position = startPosition;
    }

    IEnumerator MoveObjectForward(GameObject obj)
    {
        Vector3 forwardStartPosition = obj.transform.position;
        float journeyLength = Vector3.Distance(forwardStartPosition, targetPosition);
        float startTime = Time.time;

        while (Time.time - startTime < journeyLength / 10)
        {
            float distanceCovered = (Time.time - startTime) * 10;
            float fractionOfJourney = distanceCovered / journeyLength;

            Vector3 newPosition = Vector3.Lerp(forwardStartPosition, targetPosition, fractionOfJourney);
            // Hareket ederken bobbing efekti uygulanır
            float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            newPosition.y += bobbingOffset;

            obj.transform.position = newPosition;

            yield return null;
        }

        obj.transform.position = targetPosition;
    }

    private void CheckGameResult()
    {
        Debug.Log("TotalMoney: " + totalMoney + " LosePrice: " + losePrice); // Hata ayıklamak için log

        if (totalMoney < losePrice)
        {
            Debug.Log("Game Over"); // Eğer kaybettiyse buraya gelir
            SceneManager.LoadScene("GameOver");
        }
        else if (totalMoney >= losePrice)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            Debug.Log("Current Scene: " + currentScene); // Hangi sahnede olduğumuzu kontrol et

            if (currentScene == "SampleScene")
            {
                Debug.Log("Loading SecondScene"); // SampleScene ise SecondScene'i yükler
                SceneManager.LoadScene("SecondScene");
            }
            else
            {
                Debug.Log("Loading Startt"); // Diğer sahnelerde Startt'yi yükler
                SceneManager.LoadScene("Startt");
            }
        }
    }

    public void SetPriceCheckResult(bool result)
    {
        priceCheckResult = result;
        checkPrice = false; // Kullanıcı butona bastı, devam edebilir
        checkbutton.SetActive(false);
    }

    public void RejectAndMoveNext()
    {
        // Direkt reddet ve geri dönüp diğer objeye geç
        priceCheckResult = false;
        checkPrice = false;
    }
}
