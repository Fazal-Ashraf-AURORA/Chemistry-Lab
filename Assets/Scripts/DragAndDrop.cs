using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 offset;
    private float zDistance;

    // 3D world constraints
    public Vector3 minWorldBounds = new Vector3(-5, -5, 0);
    public Vector3 maxWorldBounds = new Vector3(5, 5, 0);

    // 2D screen constraints
    public Rect screenBounds = new Rect(0, 0, Screen.width, Screen.height);

    private Animator animator; // Reference to the Animator component

    private void Start()
    {
        animator = GetComponent<Animator>(); // Get Animator component attached to the GameObject
    }

    private void OnMouseDown()
    {
        // Disable the Animator to allow manual movement
        if (animator != null)
        {
            animator.enabled = false;
        }

        zDistance = Camera.main.WorldToScreenPoint(transform.position).z; // Store z-distance
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        offset = transform.position - mouseWorldPosition;
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        // Apply the offset
        Vector3 targetPosition = mouseWorldPosition + offset;

        // Clamp within the world bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, minWorldBounds.x, maxWorldBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minWorldBounds.y, maxWorldBounds.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minWorldBounds.z, maxWorldBounds.z);

        // Clamp within the screen bounds
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);
        screenPosition.x = Mathf.Clamp(screenPosition.x, screenBounds.xMin, screenBounds.xMax);
        screenPosition.y = Mathf.Clamp(screenPosition.y, screenBounds.yMin, screenBounds.yMax);
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, zDistance));

        // Set the constrained position
        transform.position = targetPosition;
    }

    private void OnMouseUp()
    {
        // Re-enable the Animator after dragging
        if (animator != null)
        {
            animator.enabled = true;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = zDistance; // Maintain z-distance from camera
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    private void OnDrawGizmos()
    {
        // Draw world bounds
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            (minWorldBounds + maxWorldBounds) / 2,
            maxWorldBounds - minWorldBounds
        );

        // Draw screen bounds
        Gizmos.color = Color.blue;
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(screenBounds.xMin, screenBounds.yMin, zDistance));
        Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(screenBounds.xMax, screenBounds.yMin, zDistance));
        Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(screenBounds.xMin, screenBounds.yMax, zDistance));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(screenBounds.xMax, screenBounds.yMax, zDistance));

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
