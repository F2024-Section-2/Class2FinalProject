using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleporter : MonoBehaviour
{
    [Header("Teleportation Settings")]
    [Tooltip("The name of the scene to teleport to when the player enters the portal")]
    public string targetSceneName;

    [Tooltip("Optional transition effect duration")]
    public float transitionDuration = 0.5f;

    [Tooltip("Should the portal play a sound effect on teleport?")]
    public bool playTeleportSound = true;

    [Tooltip("Audio clip to play when teleporting (if enabled)")]
    public AudioClip teleportSoundEffect;

    [Header("Visual Effects")]
    [Tooltip("Particle system to play when teleporting")]
    public ParticleSystem teleportParticles;

    private AudioSource audioSource;
    private bool hasTriggered = false;

    void Start()
    {
        // Ensure a scene name is set
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("Portal is missing a target scene name!");
        }

        // Set up audio source if sound is enabled
        if (playTeleportSound)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object is the player and the portal hasn't been triggered yet
        if (other.CompareTag("Player") && !hasTriggered)
        {
            TeleportPlayer(other.gameObject);
        }
    }

    void TeleportPlayer(GameObject player)
    {
        // Prevent multiple teleportations
        hasTriggered = true;

        // Play teleport sound if enabled
        if (playTeleportSound && teleportSoundEffect != null)
        {
            AudioSource.PlayClipAtPoint(teleportSoundEffect, transform.position);
        }

        // Play teleport particles if assigned
        if (teleportParticles != null)
        {
            teleportParticles.Play();
        }

        // Fade out and load new scene
        StartCoroutine(TeleportWithTransition(player));
    }

    System.Collections.IEnumerator TeleportWithTransition(GameObject player)
    {
        // Optional: Implement a fade transition
        // You might want to create a separate TransitionManager for more complex transitions
        float elapsedTime = 0f;
        
        // Simple fade out effect (you can replace with a more sophisticated transition)
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Load the target scene
        try 
        {
            SceneManager.LoadScene(targetSceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load scene {targetSceneName}: {e.Message}");
            hasTriggered = false; // Allow retry if scene load fails
        }
    }

    // Optional: Visual indicator in the editor
    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to show portal trigger area
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, GetComponent<Collider>().bounds.extents.magnitude);
    }
}
