using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private Vector3 initialPosition; 
    [SerializeField] private GameObject onBoardObject;
    [SerializeField] private GameObject targetObject;

    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; 
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
        transform.position = initialPosition;
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
