//using UnityEngine;

//public class Pouring : MonoBehaviour
//{
//    [SerializeField] private LiquidFillController liquidFillController;
//    [SerializeField] private FlaskInteraction flaskAInteraction;
//    [SerializeField] private FlaskInteraction flaskBInteraction;

//    [Header("Test Tube Settings")]
//    [Tooltip("The name of the test tube object to check for.")]
//    public string ColliderName = "Flask";

//    [Tooltip("Animator to control the pouring animation.")]
//    public Animator animator;

//    [Header("Liquid Adjustment Settings")]
//    [Tooltip("The amount to adjust the liquid fill.")]
//    public float changeAmount = 0.1f;

//    [Tooltip("Duration for the liquid adjustment.")]
//    public float adjustmentDuration = 1.5f;

//    private void Start()
//    {
//        // Assign the animator if not already assigned in the inspector
//        if (animator == null)
//        {
//            animator = GetComponent<Animator>();
//            if (animator == null)
//            {
//                Debug.LogError("Animator not found! Assign it in the Inspector.");
//            }
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // Check if the entering object's name matches the testTubeName (case-insensitive)
//        if (other.gameObject.name.ToLower().Contains(ColliderName.ToLower()))
//        {
//            Debug.Log("TestTube has entered the trigger!");

//            if (animator != null)
//            {
//                animator.Play("pouring");
//                flaskAInteraction.hasPoured = true;
//            }
//            else
//            {
//                Debug.LogError("Animator is not assigned!");
//            }
//        }
//        else
//        {
//            Debug.Log("Some other object has entered the trigger.");
//        }
//    }

//    public void AdjustLiquidSmoothly(bool increase)
//    {
//        if (liquidFillController != null)
//        {
//            // Adjust the liquid fill smoothly using the defined settings
//            liquidFillController.SmoothAdjustFill(increase, changeAmount, adjustmentDuration);
//        }
//        else
//        {
//            Debug.LogError("LiquidFillController is not assigned!");
//        }
//    }

//    public void DecreaseLiquid()
//    {
//        // Smoothly decrease the liquid by the defined change amount
//        AdjustLiquidSmoothly(false);
//    }

//    public void IncreaseLiquid()
//    {
//        // Smoothly increase the liquid by the defined change amount
//        AdjustLiquidSmoothly(true);
//    }


//}


using UnityEngine;

public class Pouring : MonoBehaviour
{
    [SerializeField] private LiquidFillController liquidFillController;
    [SerializeField] private FlaskInteraction flaskAInteraction;
    [SerializeField] private FlaskInteraction flaskBInteraction;

    [Header("Test Tube Settings")]
    [Tooltip("Animator to control the pouring animation.")]
    public Animator animator;

    [Header("Liquid Adjustment Settings")]
    [Tooltip("The amount to adjust the liquid fill.")]
    public float changeAmount = 0.1f;

    [Tooltip("Duration for the liquid adjustment.")]
    public float adjustmentDuration = 1.5f;

    private void Start()
    {
        // Assign the animator if not already assigned in the inspector
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator not found! Assign it in the Inspector.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Use the name of the GameObject to identify the interacting flask
        Debug.Log($"An object has entered the trigger: {other.gameObject.name}");

        if (other.gameObject.name == flaskAInteraction.gameObject.name)
        {
            Debug.Log("Flask A has interacted!");
            flaskAInteraction.hasPoured = true;

            if (animator != null)
            {
                animator.Play("pouring");
            }
            else
            {
                Debug.LogError("Animator is not assigned!");
            }
        }
        else if (other.gameObject.name == flaskBInteraction.gameObject.name)
        {
            Debug.Log("Flask B has interacted!");
            flaskBInteraction.hasPoured = true;

            if (animator != null)
            {
                animator.Play("pouring");
            }
            else
            {
                Debug.LogError("Animator is not assigned!");
            }
        }
        else
        {
            Debug.Log("An unknown object has entered the trigger.");
        }
    }

    public void AdjustLiquidSmoothly(bool increase)
    {
        if (liquidFillController != null)
        {
            // Adjust the liquid fill smoothly using the defined settings
            liquidFillController.SmoothAdjustFill(increase, changeAmount, adjustmentDuration);
        }
        else
        {
            Debug.LogError("LiquidFillController is not assigned!");
        }
    }

    public void DecreaseLiquid()
    {
        // Smoothly decrease the liquid by the defined change amount
        AdjustLiquidSmoothly(false);
    }

    public void IncreaseLiquid()
    {
        // Smoothly increase the liquid by the defined change amount
        AdjustLiquidSmoothly(true);
    }
}
