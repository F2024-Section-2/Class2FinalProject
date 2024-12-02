using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    [Tooltip("Distance at which the door will open")]
    public float triggerDistance = 3f;

    [Header("Animation References")]
    [Tooltip("Animator component controlling the door")]
    public Animator doorAnimator;

    [Tooltip("Animation parameter name for opening/closing")]
    public string openParameterName = "IsOpen";

    [Header("Optional Audio")]
    [Tooltip("Sound effect to play when door opens")]
    public AudioClip openDoorSound;

    [Tooltip("Sound effect to play when door closes")]
    public AudioClip closeDoorSound;

    private AudioSource audioSource;
    private Transform playerTransform;
    private bool isDoorOpen = false;

    void Start()
    {
        // Find the player in the scene
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Set up audio source if sound effects are assigned
        if (openDoorSound != null || closeDoorSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Validate animator reference
        if (doorAnimator == null)
        {
            Debug.LogWarning("Door Animator is not assigned. Please assign in the Inspector.");
        }
    }

    void Update()
    {
        // Check if player and animator are available
        if (playerTransform == null || doorAnimator == null) return;

        // Calculate distance between door and player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Determine if player is close enough to trigger door
        if (distanceToPlayer <= triggerDistance)
        {
            if (!isDoorOpen)
            {
                OpenDoor();
            }
        }
        else
        {
            if (isDoorOpen)
            {
                CloseDoor();
            }
        }
    }

    void OpenDoor()
    {
        // Set animator parameter to open
        doorAnimator.SetBool(openParameterName, true);
        isDoorOpen = true;

        // Play open sound if available
        if (audioSource != null && openDoorSound != null)
        {
            audioSource.PlayOneShot(openDoorSound);
        }
    }

    void CloseDoor()
    {
        // Set animator parameter to close
        doorAnimator.SetBool(openParameterName, false);
        isDoorOpen = false;

        // Play close sound if available
        if (audioSource != null && closeDoorSound != null)
        {
            audioSource.PlayOneShot(closeDoorSound);
        }
    }

    // Optional: Visualize trigger distance in scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}
