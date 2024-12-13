using System.Collections;
using UnityEngine;

public class EntitiesMovement : MonoBehaviour
{
    public GameObject[] objects;
    private int currentObjectIndex = 0;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;

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

        startPosition = obj.transform.position;
        targetPosition = startPosition + new Vector3(16.2f, 0, 0);

        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (Time.time - startTime < journeyLength / 10)
        {
            float distanceCovered = (Time.time - startTime) * 10;
            float fractionOfJourney = distanceCovered / journeyLength;
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        obj.transform.position = targetPosition;

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
            obj.transform.position = Vector3.Lerp(backStartPosition, startPosition, fractionOfJourney);
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

        // Ýleri hareket ediyor
        while (Time.time - startTime < journeyLength / 10)
        {
            float distanceCovered = (Time.time - startTime) * 10;
            float fractionOfJourney = distanceCovered / journeyLength;
            obj.transform.position = Vector3.Lerp(forwardStartPosition, targetPosition, fractionOfJourney);
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
