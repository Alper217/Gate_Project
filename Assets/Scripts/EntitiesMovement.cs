using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntitiesMovement : MonoBehaviour
{
    public GameObject[] objects; // Objeler listesi
    public int currentObjectIndex = 0; // Þu anki obje indeksi
    private bool isMoving = false; // Hareket kontrolü

    private Vector3 startPosition; // Baþlangýç pozisyonu
    private Vector3 targetPosition; // Hedef pozisyon

    public float bobbingAmplitude = 0.2f; // Yüksekliðin genliði (yukarý-aþaðý hareket miktarý)
    public float bobbingSpeed = 3f; // Yüksekliðin hýzýný ayarlar

    private Renderer objRenderer;
    private Color originalColor;

    public bool checkPrice = false;
    public GameObject checkbutton;
    public GameObject CikisButonu; // Yeni Çýkýþ Butonu

    private bool priceCheckResult = false; // Fiyat kontrol sonucu

    [SerializeField] private GameObject animatedObject; // Animasyon yapacak GameObject

    void Start()
    {
        // CikisButonu baþta inaktif
        if (CikisButonu != null)
        {
            CikisButonu.SetActive(false);
        }
    }

    void Update()
    {
        // Update içerisine harekete dair bir iþlem eklenmedi
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

        while (Time.time - startTime < journeyLength / 10)
        {
            float distanceCovered = (Time.time - startTime) * 10;
            float fractionOfJourney = distanceCovered / journeyLength;

            // Yatay hareket
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

            obj.transform.position = newPosition;

            // Renk geçiþi
            objRenderer.material.color = Color.Lerp(Color.black, originalColor, fractionOfJourney);

            yield return null;
        }

        obj.transform.position = targetPosition;
        objRenderer.material.color = originalColor;

        checkPrice = true;
        checkbutton.SetActive(true); // Buton hareket bitince aktif
        CikisButonu.SetActive(true); // Çýkýþ Butonunu aktif et

        // Kullanýcý butona basana kadar bekle
        yield return new WaitUntil(() => !checkPrice);

        CikisButonu.SetActive(false); // Karakter hareket ederken çýkýþ butonunu inaktif yap

        // Fiyat kontrol sonucuna göre hareket et
        if (priceCheckResult)
        {
            targetPosition = targetPosition + new Vector3(16.8f, 0, 0);
            yield return StartCoroutine(MoveObjectForward(obj));
        }
        else
        {
            targetPosition = startPosition;
            yield return StartCoroutine(MoveObjectBack(obj));
        }

        // Sonraki objeye geçiþ
        currentObjectIndex++;
        if (currentObjectIndex < objects.Length)
        {
            isMoving = false; // Sonraki hareket için tekrar týklamayý bekle
            CikisButonu.SetActive(true); // Sýradaki karakter geldiðinde buton tekrar aktif
        }
        else
        {
            isMoving = false;
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

            // Hareket ederken bobbing efekti uygulanýr
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
            // Hareket ederken bobbing efekti uygulanýr
            float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            newPosition.y += bobbingOffset;

            obj.transform.position = newPosition;

            yield return null;
        }

        obj.transform.position = targetPosition;
    }

    private IEnumerator AnimateObject(GameObject obj)
    {
        Vector3 originalPosition = obj.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, -2, 0);

        // Yukarý çýkma
        float elapsedTime = 0f;
        float duration = 0.5f; // Animasyon süresi

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pozisyonu tamamen hedef pozisyona ayarla
        obj.transform.position = targetPosition;

        // Aþaðý dönme
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pozisyonu tamamen baþlangýç pozisyonuna ayarla
        obj.transform.position = originalPosition;
    }

    public void SetPriceCheckResult(bool result)
    {
        priceCheckResult = result;
        checkPrice = false; // Kullanýcý butona bastý, devam edebilir
        checkbutton.SetActive(false);
    }

    public void RejectAndMoveNext()
    {
        // Direkt reddet ve geri dönüp diðer objeye geç
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
