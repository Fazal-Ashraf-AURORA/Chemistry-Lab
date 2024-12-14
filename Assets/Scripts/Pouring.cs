using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouring : MonoBehaviour
{
    [Header("TestTube Settings")]
    public string testTubeName = "TestTube"; // The name of the test tube object to check for
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
       // animator.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object's name matches the testTubeName (case-insensitive)
        if (other.gameObject.name.ToLower().Contains(testTubeName.ToLower()))
        {
            Debug.Log("TestTube has entered the trigger!");
            //animator.enabled = true;
            //animator.SetTrigger("Pour");
            animator.Play("pouring");
        }
        else
        {
            Debug.Log("Some other object has entered the trigger.");
        }
    }

    public void DisableAnimator()
    {
        //animator.enabled = false;
    }
}
