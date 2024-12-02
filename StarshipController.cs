using UnityEngine;

public class StarshipController : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Pitch rotation speed (up/down)")]
    public float pitchSpeed = 100f;

    [Tooltip("Yaw rotation speed (left/right)")]
    public float yawSpeed = 100f;

    [Header("Propulsion Settings")]
    [Tooltip("Propulsion force when mouse is clicked")]
    public float propulsionForce = 10f;

    [Tooltip("Maximum speed to prevent infinite acceleration")]
    public float maxSpeed = 20f;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to this starship
        rb = GetComponent<Rigidbody>();

        // Ensure the Rigidbody is set to not use gravity
        rb.useGravity = false;
    }

    void Update()
    {
        // Handle Rotation
        float pitchInput = 0f;
        float yawInput = 0f;

        // Pitch Control (W/S keys)
        if (Input.GetKey(KeyCode.W))
        {
            pitchInput = 1f; // Pitch Up
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pitchInput = -1f; // Pitch Down
        }

        // Yaw Control (A/D keys)
        if (Input.GetKey(KeyCode.A))
        {
            yawInput = -1f; // Yaw Left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            yawInput = 1f; // Yaw Right
        }

        // Apply rotations
        // Pitch rotation around local X-axis
        transform.Rotate(Vector3.right * pitchInput * pitchSpeed * Time.deltaTime, Space.Self);
        
        // Yaw rotation around local Y-axis
        transform.Rotate(Vector3.up * yawInput * yawSpeed * Time.deltaTime, Space.Self);

        // Handle Propulsion on Mouse Click
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            // Calculate propulsion direction (forward of the ship)
            Vector3 propulsionDirection = transform.forward;

            // Apply force in the direction the ship is facing
            rb.AddForce(propulsionDirection * propulsionForce, ForceMode.Force);

            // Limit maximum speed
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}
