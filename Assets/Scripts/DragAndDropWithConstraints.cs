using UnityEngine;

public class DragAndDropWithConstraints : MonoBehaviour
{
    public Camera customCamera; // Assign the camera to be used
    public Vector3 minWorldBounds; // Minimum world position (3D constraints)
    public Vector3 maxWorldBounds; // Maximum world position (3D constraints)
    public Rect screenBounds; // 2D screen space constraints

    private Vector3 offset; // Offset between object position and mouse position
    private float zDistance; // Distance from camera to object

    private void Awake()
    {
        if (customCamera == null)
        {
            customCamera = Camera.main; // Fallback to main camera if not assigned
        }
    }

    private void OnMouseDown()
    {
        zDistance = customCamera.WorldToScreenPoint(transform.position).z; // Store z-distance
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        offset = transform.position - mouseWorldPosition;
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        // Apply the offset and clamp within the world bounds
        Vector3 targetPosition = mouseWorldPosition + offset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minWorldBounds.x, maxWorldBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minWorldBounds.y, maxWorldBounds.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minWorldBounds.z, maxWorldBounds.z);

        // Apply 2D screen bounds
        Vector3 screenPosition = customCamera.WorldToScreenPoint(targetPosition);
        screenPosition.x = Mathf.Clamp(screenPosition.x, screenBounds.xMin, screenBounds.xMax);
        screenPosition.y = Mathf.Clamp(screenPosition.y, screenBounds.yMin, screenBounds.yMax);
        targetPosition = customCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, zDistance));

        // Set the constrained position
        transform.position = targetPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = zDistance; // Maintain z-distance from camera
        return customCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
