using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private Vector3 initialPosition; // Baþlangýç pozisyonu
    [SerializeField] private GameObject onBoardObject; // Masadaki obje
    [SerializeField] private GameObject targetObject;  // Sürüklenebilir obje

    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; // Sürüklenebilir objenin baþlangýç pozisyonunu kaydet
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        // Obje býrakýldýðýnda baþlangýç pozisyonuna döndür
        transform.position = initialPosition;

        // Masa objesini aç, sürüklenebilir objeyi kapat
        if (onBoardObject != null && targetObject != null)
        {
            onBoardObject.SetActive(true);
            targetObject.SetActive(false);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
