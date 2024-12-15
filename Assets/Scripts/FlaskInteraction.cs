using UnityEngine;

public class FlaskInteraction : MonoBehaviour
{
    [Header("Flask Settings")]
    [Tooltip("The name of the flask to check for interaction.")]
    public string flaskName = "Flask"; // Name of the flask to interact with

    public float shakeDuration = 0.5f; // Duration of the shake
    public float rotationAngle = 15f; // Maximum rotation angle for the shake

    [Header("Liquid Color Settings")]
    [SerializeField] private Material liquidMaterial; // Material of the liquid
    [SerializeField] private string targetColorHex; // Target color in hex format
    [SerializeField] private float colorLerpDuration = 1f; // Time to lerp the color

    private bool isShaking = false;
    private float shakeElapsedTime = 0f;
    private Quaternion originalRotation;

    private Color initialColor;
    private Color targetColor;
    private float colorLerpElapsedTime;

    [Header("States")]
    public bool hasPoured = false;
    public bool reactionComplete = false;

    private void Start()
    {
        // Validate and parse the target color
        if (!ColorUtility.TryParseHtmlString(targetColorHex, out targetColor))
        {
            Debug.LogError("Invalid color hex code!");
            enabled = false;
        }

        if (liquidMaterial != null)
        {
            initialColor = liquidMaterial.GetColor("_Tint");
        }
        else
        {
            Debug.LogError("Liquid Material is not assigned!");
        }
    }

    void Update()
    {
        // Check for right mouse click (for shaking flask)
        if (Input.GetMouseButtonDown(1)) // 1 corresponds to the right mouse button
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object's name matches the specified flask name
                if (hit.collider.gameObject.name.Equals(flaskName, System.StringComparison.OrdinalIgnoreCase))
                {
                    // Start shaking the flask
                    if (!isShaking)
                    {
                        originalRotation = hit.collider.transform.rotation;
                        isShaking = true;
                        shakeElapsedTime = 0f;
                    }
                }
            }
        }

        // Handle shaking independently of pouring
        if (isShaking)
        {
            ShakeObject();
        }

        // Handle color lerp only after pouring and shaking
        if (hasPoured && isShaking)
        {
            LerpLiquidColor();
        }
    }

    private void ShakeObject()
    {
        shakeElapsedTime += Time.deltaTime;

        if (shakeElapsedTime < shakeDuration)
        {
            // Calculate the rotation offset based on a sine wave for smooth oscillation
            float angleX = Mathf.Sin(Time.time * Mathf.PI * 4f) * rotationAngle;
            float angleZ = Mathf.Sin(Time.time * Mathf.PI * 4f + Mathf.PI / 2) * rotationAngle;

            // Apply the rotation
            transform.rotation = originalRotation * Quaternion.Euler(angleX, 0f, angleZ);
        }
        else
        {
            // Stop shaking and reset rotation
            isShaking = false;
            transform.rotation = originalRotation;
        }
    }

    private void LerpLiquidColor()
    {
        if (liquidMaterial != null)
        {
            colorLerpElapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(colorLerpElapsedTime / colorLerpDuration);
            liquidMaterial.SetColor("_Tint", Color.Lerp(initialColor, targetColor, t));
            reactionComplete = true;

            if(reactionComplete)
                Debug.Log("Color Turned");
        }
    }

    // Reset the liquid color when exiting play mode or disabling the script
    private void OnDisable()
    {
        if (liquidMaterial != null)
        {
            liquidMaterial.SetColor("_Tint", initialColor); // Restore original color
        }
    }
}
