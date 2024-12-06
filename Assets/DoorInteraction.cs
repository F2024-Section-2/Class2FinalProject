using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animation doorAnimation; // Reference to the Animation component
    public string openAnimationName = "DoorOpen"; // Animation for opening the door
    public string closeAnimationName = "DoorClose"; // Animation for closing the door
    public Transform entryPoint; // Position where the player moves after entering
    public float entryDelay = 1.0f; // Delay before the player moves inside

    private bool isDoorOpen = false; // Tracks door state
    private bool isPlayerNear = false; // Tracks if the player is near

    private void Update()
    {
        // Check for interaction input if the player is near
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDoorOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    private void OpenDoor()
    {
        if (!doorAnimation.isPlaying)
        {
            doorAnimation.Play(openAnimationName); // Play the door open animation
            isDoorOpen = true; // Update state
            Invoke(nameof(EnterDoor), entryDelay); // Delay before moving player inside
        }
    }

    private void CloseDoor()
    {
        if (!doorAnimation.isPlaying)
        {
            doorAnimation.Play(closeAnimationName); // Play the door close animation
            isDoorOpen = false; // Update state
        }
    }

    private void EnterDoor()
    {
        if (entryPoint != null)
        {
            // Move the player to the specified entry point
            Transform player = GameObject.FindWithTag("Player").transform;
            player.position = entryPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true; // Player is in range
            Debug.Log("Press 'E' to open/close the door.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false; // Player left the range
        }
    }
}
