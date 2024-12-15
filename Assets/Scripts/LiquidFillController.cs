using UnityEngine;

public class LiquidFillController : MonoBehaviour
{
    [SerializeField] private Material liquidMaterial; // Assign the material using the shader
    [SerializeField] private float minFill = -1f; // Minimum fill level
    [SerializeField] private float maxFill = 1f; // Maximum fill level
    private float currentFill;

    private void Awake()
    {
        if (liquidMaterial != null)
        {
            // Initialize `currentFill` to 0.33f and set it on the material
            currentFill = Mathf.Clamp(0.33f, minFill, maxFill);
            liquidMaterial.SetFloat("_FillAmount", currentFill);
        }
        else
        {
            Debug.LogError("Liquid Material is not assigned!");
        }
    }

    public void AdjustFill(bool increase, float changeAmount)
    {
        if (liquidMaterial != null)
        {
            // Adjust `currentFill` and update the material
            currentFill += increase ? changeAmount : -changeAmount;
            currentFill = Mathf.Clamp(currentFill, minFill, maxFill);
            liquidMaterial.SetFloat("_FillAmount", currentFill);
        }
        else
        {
            Debug.LogError("Liquid Material is not assigned!");
        }
    }

    public void SmoothAdjustFill(bool increase, float changeAmount, float duration)
    {
        if (liquidMaterial != null)
        {
            // Calculate the target fill
            float targetFill = currentFill + (increase ? changeAmount : -changeAmount);
            // Clamp the target fill within bounds
            targetFill = Mathf.Clamp(targetFill, minFill, maxFill);
            // Start the lerp coroutine
            StartCoroutine(LerpFillCoroutine(targetFill, duration));
        }
        else
        {
            Debug.LogError("Liquid Material is not assigned!");
        }
    }

    private System.Collections.IEnumerator LerpFillCoroutine(float targetFill, float duration)
    {
        float startFill = currentFill;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentFill = Mathf.Lerp(startFill, targetFill, elapsed / duration);
            liquidMaterial.SetFloat("_FillAmount", currentFill);
            yield return null;
        }

        // Ensure the final value is set
        currentFill = targetFill;
        liquidMaterial.SetFloat("_FillAmount", currentFill);
    }
}
