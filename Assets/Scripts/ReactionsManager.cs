using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionsManager : MonoBehaviour
{
    public static ReactionsManager instance;

    [Header("Reaction Status")]
    [SerializeField] private FlaskInteraction Flask_A_Interaction;
    [SerializeField] private FlaskInteraction Flask_B_Interaction;

    public bool Flask_A_ReacionComplete;
    public bool Flask_B_ReacionComplete;

    public Animator playerAnimator; // Animator for the player

    [Header("Effects")]
    public ParticleSystem unpleasantSmellEffectPrefab; // Prefab for the unpleasant smell effect
    public Transform flaskBEffectPosition; // Position to instantiate the effect
    public AudioSource audioSource; // Audio source for playing sounds
    public AudioClip unpleasantSmellSound;

    // Flags to ensure animations are played only once
    private bool flaskAAnimationPlayed = false;
    private bool flaskBAnimationPlayed = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        Flask_A_ReacionComplete = Flask_A_Interaction.reactionComplete;
        Flask_B_ReacionComplete = Flask_B_Interaction.reactionComplete;

        // Check if Flask A's reaction is complete and animation hasn't been played yet
        if (Flask_A_ReacionComplete && !flaskAAnimationPlayed)
        {
            Debug.Log("Flask_A_ReacionComplete");
            playerAnimator.Play("Excited");
            flaskAAnimationPlayed = true; // Mark animation as played
        }

        // Check if Flask B's reaction is complete and animation hasn't been played yet
        if (Flask_B_ReacionComplete && !flaskBAnimationPlayed)
        {
            Debug.Log("Flask_B_ReacionComplete");

            // Play "Irritated" animation
            playerAnimator.Play("Irritated");

            // Instantiate the unpleasant smell effect
            if (unpleasantSmellEffectPrefab != null && flaskBEffectPosition != null)
            {
                Instantiate(unpleasantSmellEffectPrefab, flaskBEffectPosition.position, Quaternion.identity);
                Debug.Log("Unpleasant smell effect instantiated.");
            }
            else
            {
                Debug.LogError("Unpleasant smell effect prefab or position is not assigned!");
            }

            // Play the unpleasant smell sound
            if (audioSource != null && unpleasantSmellSound != null)
            {
                audioSource.PlayOneShot(unpleasantSmellSound);
                Debug.Log("Unpleasant smell sound played.");
            }
            else
            {
                Debug.LogError("AudioSource or unpleasant smell sound clip is not assigned!");
            }

            flaskBAnimationPlayed = true; // Mark animation and effects as played
        }
    }
}
