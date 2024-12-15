using UnityEngine;

public class LiquidManager : MonoBehaviour
{
    [SerializeField] private LiquidColorLerp liquidColorLerp;

    private void Start()
    {
        // Example of calling StartLerp externally
        if (liquidColorLerp != null)
        {
            // Change the liquid color to blue (#0000FF) over 2 seconds
            liquidColorLerp.StartLerp("#0000FF", 6f);
        }
        else
        {
            Debug.LogError("LiquidColorLerp is not assigned!");
        }
    }
}
