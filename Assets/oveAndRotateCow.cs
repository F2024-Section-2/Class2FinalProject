using UnityEngine;

public class oveAndRotateCow : MonoBehaviour
{
    // Movement speed (how fast the cow moves forward/backward)
    public float moveSpeed = 5f;

    // Distance to move forward/backward (20 steps)
    public float moveDistance = 20f;

    // Track how much distance the cow has moved
    private float distanceMoved = 0f;

    // Track if the cow is moving forward or backward
    private bool movingForward = true;

    // Timer to track the delay before rotating
    private bool isPaused = false;
    private float pauseTime = 3f;

    private void Update()
    {
        // Move the cow based on the direction it's moving
        if (!isPaused)
        {
            MoveCow();
        }
    }

    private void MoveCow()
    {
        if (movingForward)
        {
            // Move the cow forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            distanceMoved += moveSpeed * Time.deltaTime;

            // If the cow has moved the full distance, stop and start the pause
            if (distanceMoved >= moveDistance)
            {
                // Stop the cow and start the pause
                isPaused = true;
                Invoke("StartRotation", pauseTime);  // After 3 seconds, call StartRotation
            }
        }
        else
        {
            // Move the cow backward (in the opposite direction)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); // Move in the reverse direction
            distanceMoved -= moveSpeed * Time.deltaTime;

            // If the cow has returned to the start position, stop and start the pause
            if (distanceMoved <= 0f)
            {
                // Stop the cow and start the pause
                isPaused = true;
                Invoke("StartRotation", pauseTime);  // After 3 seconds, call StartRotation
            }
        }
    }

    private void StartRotation()
    {
        // Rotate the cow 180 degrees to face the opposite direction
        transform.Rotate(0, 180, 0);

        // After rotating, reset the movement and resume the cycle
        isPaused = false;

        // Switch direction for the next move
        movingForward = !movingForward;
    }
}
