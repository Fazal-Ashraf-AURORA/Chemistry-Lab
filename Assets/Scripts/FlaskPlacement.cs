using UnityEngine;

public class FlaskPlacement : MonoBehaviour
{
    [Header("Placement Settings")]
    public Transform targetPosition; // The exact position where the flask should be placed

    private Camera mainCamera; // Reference to the main camera for raycasting
    private GameObject pickedObject; // The currently picked flask
    private Vector3 offset; // Offset to maintain the relative position during dragging

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            TryPickObject();
        }
        else if (Input.GetMouseButton(0) && pickedObject != null) // Dragging the flask
        {
            DragObject();
        }
        else if (Input.GetMouseButtonUp(0) && pickedObject != null) // Release the flask
        {
            ReleaseObject();
        }
    }

    private void TryPickObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.name.Contains("Flask")) // Check if the object is a flask
            {
                pickedObject = hit.collider.gameObject;
                offset = pickedObject.transform.position - hit.point;

                // Enable physics for the flask to allow re-picking
                Rigidbody flaskRigidbody = pickedObject.GetComponent<Rigidbody>();
                if (flaskRigidbody != null)
                {
                    flaskRigidbody.isKinematic = false;
                }

                Debug.Log($"Picked up {pickedObject.name}");
            }
        }
    }

    private void DragObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            pickedObject.transform.position = hit.point + offset;
        }
    }

    private void ReleaseObject()
    {
        if (Vector3.Distance(pickedObject.transform.position, targetPosition.position) < 0.5f) // Snap condition
        {
            pickedObject.transform.position = targetPosition.position;
            pickedObject.transform.rotation = targetPosition.rotation;

            // Optionally, disable physics to lock the flask in place
            Rigidbody flaskRigidbody = pickedObject.GetComponent<Rigidbody>();
            if (flaskRigidbody != null)
            {
                flaskRigidbody.isKinematic = true;
            }

            Debug.Log($"{pickedObject.name} placed on the table.");
        }
        else
        {
            Debug.Log($"{pickedObject.name} released but not placed on the table.");
        }

        pickedObject = null; // Release the object
    }
}
