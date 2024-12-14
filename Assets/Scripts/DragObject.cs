using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private Vector3 initialPosition; // Ba�lang�� pozisyonu
    [SerializeField] private GameObject onBoardObject; // Masadaki obje
    [SerializeField] private GameObject targetObject;  // S�r�klenebilir obje

    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; // S�r�klenebilir objenin ba�lang�� pozisyonunu kaydet
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
        // Obje b�rak�ld���nda ba�lang�� pozisyonuna d�nd�r
        transform.position = initialPosition;

        // Masa objesini a�, s�r�klenebilir objeyi kapat
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
