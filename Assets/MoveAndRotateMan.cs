using UnityEngine;

public class MoveAndRotateMan : MonoBehaviour
{
    // Movement speed (how fast the man moves forward)
    public float moveSpeed = 5f;

    // Rotation speed (how fast the man rotates)
    public float rotationSpeed = 100f;

    // The distance to move forward (you can tweak this)
    public float moveDistance = 5f;

    private Vector3 initialPosition;

    private void Start()
    {
        // Save the initial position to measure distance later
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Move the man object forward (in the direction of the object's forward vector)
        MoveMan();

        // Rotate the man object
        RotateMan();
    }

    private void MoveMan()
    {
        // Move forward along the z-axis and check if the object has moved the desired distance
        if (Vector3.Distance(initialPosition, transform.position) < moveDistance)
        {
            // Move the object forward along its local forward axis
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Movement finished.");
        }
    }

    private void RotateMan()
    {
        // Rotate the object around the y-axis based on user input
        float horizontalInput = Input.GetAxis("Horizontal"); // A and D keys or Left/Right arrow keys
        float verticalInput = Input.GetAxis("Vertical"); // W and S keys or Up/Down arrow keys

        // Apply rotation on the Y-axis (rotate left or right)
        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
    }
}
