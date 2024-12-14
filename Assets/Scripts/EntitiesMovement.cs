using System.Collections;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            StartCoroutine(MoveObject(objects[currentObjectIndex]));
        }
    }

    IEnumerator MoveObject(GameObject obj)
    {
        isMoving = true;

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

            // Yukarý-aþaðý hareket (bobbing effect) ekleniyor
            float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            newPosition.y += bobbingOffset;

            obj.transform.position = newPosition;
            checkPrice = true;
            checkbutton.SetActive(true);
            // Renk geçiþi
            objRenderer.material.color = Color.Lerp(Color.black, originalColor, fractionOfJourney);

            yield return null;
        }

        obj.transform.position = targetPosition;
        objRenderer.material.color = originalColor;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D));

        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPosition = startPosition;
            StartCoroutine(MoveObjectBack(obj));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            targetPosition = targetPosition + new Vector3(16.8f, 0, 0);
            StartCoroutine(MoveObjectForward(obj));
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

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        currentObjectIndex++;

        if (currentObjectIndex < objects.Length)
        {
            StartCoroutine(MoveObject(objects[currentObjectIndex]));
        }
        else
        {
            isMoving = false;
        }
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

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        currentObjectIndex++;

        if (currentObjectIndex < objects.Length)
        {
            StartCoroutine(MoveObject(objects[currentObjectIndex]));
        }
        else
        {
            isMoving = false;
        }
    }
}
