using UnityEngine;

public class LiquidColorLerp : MonoBehaviour
{
    [SerializeField] private Material liquidMaterial; // Assign the material using the shader
    [SerializeField] private string targetColorHex; // Target color in hex format
    [SerializeField] private float duration = 1f; // Time to lerp the color

    private Color targetColor;
    private Color initialColor;
    private float elapsedTime;
    private bool isLerping;

    private void Start()
    {
        if (!ColorUtility.TryParseHtmlString(targetColorHex, out targetColor))
        {
            Debug.LogError("Invalid color hex code!");
            enabled = false;
        }
    }

    public void StartLerp(string newColorHex, float lerpDuration)
    {
        if (liquidMaterial != null)
        {
            // Parse the new color from the hex string
            if (ColorUtility.TryParseHtmlString(newColorHex, out targetColor))
            {
                initialColor = liquidMaterial.GetColor("_Tint");
                duration = lerpDuration;
                elapsedTime = 0f;
                isLerping = true;
            }
            else
            {
                Debug.LogError("Invalid color hex code!");
            }
        }
        else
        {
            Debug.LogError("Liquid Material is not assigned!");
        }
    }

    private void Update()
    {
        if (isLerping)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            liquidMaterial.SetColor("_Tint", Color.Lerp(initialColor, targetColor, t));

            if (t >= 1f)
            {
                isLerping = false;
            }
        }
    }
}
