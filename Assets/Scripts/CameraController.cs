using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 1.0f;
    [SerializeField] private float zoomSpeed = 0.01f;
    [SerializeField] private float minOrthoSize = 3.0f;
    [SerializeField] private float maxOrthoSize = 12.0f;
    [SerializeField] private Vector2 mapHalfExtents = new Vector2(15f,15f);

    private Camera controlledCamera;
    private Vector3 lastPanWorldPosition;
    private bool isPanning;
    private void Awake()
    {
        controlledCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.touchCount == 2) //pinch
        {
            HandlePinchZoom();
        }
        else if (Input.touchCount == 1) //Normal touch
        {
            HandlePan(Input.GetTouch(0).position, Input.GetTouch(0).phase == TouchPhase.Began);
        }
        else if (Input.mousePresent)
        {
            HandleMouse();
        }
        ClampToMap();
    }

    private void HandlePan(Vector2 screenPosition, bool panJustStarted)
    {
        Vector3 worldPosition = controlledCamera.ScreenToWorldPoint(screenPosition);
        if (panJustStarted)
        {
            isPanning = true;
        }
        else if (isPanning)
        {
            Vector3 panDelta = lastPanWorldPosition - worldPosition;
            transform.position += panDelta * panSpeed;
            worldPosition = controlledCamera.ScreenToWorldPoint(screenPosition);
        }
        lastPanWorldPosition = worldPosition;
    }

    private void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandlePan(Input.mousePosition, true);
        }
        else if (Input.GetMouseButton(0))
        {
            HandlePan(Input.mousePosition, false);
        }
        else
        {
            isPanning = false;
        }
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0f)
        {
            controlledCamera.orthographicSize = Mathf.Clamp(controlledCamera.orthographicSize - scroll, 
                minOrthoSize, maxOrthoSize);
        }
    }

    private void ClampToMap()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -mapHalfExtents.x, mapHalfExtents.x);
        float clampedY = Mathf.Clamp(transform.position.y, -mapHalfExtents.y, mapHalfExtents.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void HandlePinchZoom()
    {
        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);
        float currentSeparation = Vector2.Distance(firstTouch.position, secondTouch.position);
        float previousSeparation = Vector2.Distance(
            firstTouch.position - firstTouch.deltaPosition,
            secondTouch.position - secondTouch.deltaPosition
        );
        float zoomDelta = (previousSeparation - currentSeparation) * zoomSpeed;
        controlledCamera.orthographicSize = Mathf.Clamp(controlledCamera.orthographicSize + zoomDelta,
            minOrthoSize, maxOrthoSize);
    }
}
