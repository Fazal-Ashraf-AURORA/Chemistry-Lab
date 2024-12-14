using UnityEngine;

public class FlaskPlacement : MonoBehaviour
{
    [Header("Placement Settings")]
    public Transform targetPosition; // The exact position where the flask should be placed
    public string flaskNameContains = "Flask"; // The name or part of the name to identify flask objects (case-insensitive)

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object's name contains the specified string (case-insensitive comparison)
        if (other.gameObject.name.ToLower().Contains(flaskNameContains.ToLower()))
        {
            GameObject flask = other.gameObject;

            // Snap the flask to the target position
            flask.transform.position = targetPosition.position;
            //flask.transform.rotation = targetPosition.rotation;

            //// Optionally, disable physics to lock the flask in place
            //Rigidbody flaskRigidbody = flask.GetComponent<Rigidbody>();
            //if (flaskRigidbody != null)
            //{
            //    flaskRigidbody.isKinematic = true;
            //}

            Debug.Log($"{flask.name} placed on the table.");
        }
    }
}
