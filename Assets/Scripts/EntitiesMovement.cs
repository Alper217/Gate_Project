using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntitiesMovement : MonoBehaviour
{
    [SerializeField] int losePrice;
    public GameObject[] objects; // Objeler listesi
    public int currentObjectIndex = 0; // �u anki obje indeksi
    private bool isMoving = false; // Hareket kontrol�

    private Vector3 startPosition; // Ba�lang�� pozisyonu
    private Vector3 targetPosition; // Hedef pozisyon

    public float bobbingAmplitude = 0.2f; // Y�ksekli�in genli�i (yukar�-a�a�� hareket miktar�)
    public float bobbingSpeed = 3f; // Y�ksekli�in h�z�n� ayarlar

    private Renderer objRenderer;
    private Color originalColor;

    public bool checkPrice = false;
    public GameObject checkbutton;
    public GameObject CikisButonu; // Yeni ��k�� Butonu

    private bool priceCheckResult = false; // Fiyat kontrol sonucu

    [SerializeField] private GameObject animatedObject; // Animasyon yapacak GameObject
    [SerializeField] public GameObject[] dialogBoxes;

    private int totalMoney = 0; // Toplam para miktar�

    void Start()
    {
        // CikisButonu ba�ta inaktif
        if (CikisButonu != null)
        {
            CikisButonu.SetActive(false);
        }
    }

    void Update()
    {
        // Update i�erisine harekete dair bir i�lem eklenmedi
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

        // Diyalog box'� ilk karakterin hareketi tamamland�ktan sonra aktif et
        if (dialogBoxes.Length > currentObjectIndex)
        {
            dialogBoxes[currentObjectIndex].SetActive(true); // �lgili diyalog box'� aktif et
        }

        while (Time.time - startTime < journeyLength / 10)
        {
            float distanceCovered = (Time.time - startTime) * 10;
            float fractionOfJourney = distanceCovered / journeyLength;

            // Yatay hareket
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

            obj.transform.position = newPosition;

            // Renk ge�i�i
            objRenderer.material.color = Color.Lerp(Color.black, originalColor, fractionOfJourney);

            yield return null;
        }

        obj.transform.position = targetPosition;
        objRenderer.material.color = originalColor;

        checkPrice = true;
        checkbutton.SetActive(true); // Buton hareket bitince aktif
        CikisButonu.SetActive(true); // ��k�� Butonunu aktif et

        // Kullan�c� butona basana kadar bekle
        yield return new WaitUntil(() => !checkPrice);

        // �nceki diyalog kutusunu inaktif yap
        if (dialogBoxes.Length > currentObjectIndex)
        {
            dialogBoxes[currentObjectIndex].SetActive(false); // �nceki diyalog box'�n� inaktif et
        }

        CikisButonu.SetActive(false); // Karakter hareket ederken ��k�� butonunu inaktif yap

        // Fiyat kontrol sonucuna g�re hareket et
        if (priceCheckResult)
        {
            totalMoney += 5; // E�er fiyat kontrol� olumluysa, para miktar�n� art�r
            targetPosition = targetPosition + new Vector3(16.8f, 0, 0);
            yield return StartCoroutine(MoveObjectForward(obj));
        }
        else
        {
            targetPosition = startPosition;
            yield return StartCoroutine(MoveObjectBack(obj));
        }

        // Sonraki objeye ge�i�
        currentObjectIndex++;
        if (currentObjectIndex < objects.Length)
        {
            isMoving = false; // Sonraki hareket i�in tekrar t�klamay� bekle
            CikisButonu.SetActive(true); // S�radaki karakter geldi�inde buton tekrar aktif
        }
        else
        {
            isMoving = false;
            CheckGameResult(); // T�m karakterler hareket ettikten sonra oyunu kontrol et
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

            // Hareket ederken bobbing efekti uygulan�r
            float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            newPosition.y += bobbingOffset;

            obj.transform.position = newPosition;

            yield return null;
        }

        obj.transform.position = startPosition;
    }
    private IEnumerator AnimateObject(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("AnimateObject: Animasyon yap�lacak obje atanmad�.");
            yield break;
        }

        Vector3 originalPosition = obj.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, -2, 0); // A�a�� y�nde hareket i�in hedef pozisyon

        float duration = 0.5f; // Animasyon s�resi
        float elapsedTime = 0f;

        // A�a�� hareket
        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = targetPosition;

        // Yukar� hareket
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = originalPosition;
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
            // Hareket ederken bobbing efekti uygulan�r
            float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            newPosition.y += bobbingOffset;

            obj.transform.position = newPosition;

            yield return null;
        }

        obj.transform.position = targetPosition;
    }

    private void CheckGameResult()
    {
        if (totalMoney < losePrice)
        {
            SceneManager.LoadScene("GameOver");                      
        }
        else
        {
            Debug.Log("Kazand�n!");
        }
    }

    public void SetPriceCheckResult(bool result)
    {
        priceCheckResult = result;
        checkPrice = false; // Kullan�c� butona bast�, devam edebilir
        checkbutton.SetActive(false);
    }

    public void RejectAndMoveNext()
    {
        // Direkt reddet ve geri d�n�p di�er objeye ge�
        priceCheckResult = false;
        checkPrice = false;
        if (animatedObject != null)
        {
            StartCoroutine(AnimateObject(animatedObject));
        }
        else
        {
            Debug.LogError("Animated Object is not assigned in the Inspector.");
        }
    }
}
