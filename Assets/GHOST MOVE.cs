
using UnityEngine;

public class MoveTowardsAndBounce : MonoBehaviour
{
    public Transform target; // The object to move towards
    public float speed = 1.0f; // Speed of movement towards the target
    public float bounceHeight = 1.0f; // Height of the up and down movement
    public float bounceSpeed = 2.0f; // Speed of the up and down movement

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Calculate the new Y position for the bounce effect
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

        // Apply the new Y position while keeping the X and Z positions moving towards the target
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with has the tag "WEAPON"
        if (collision.gameObject.CompareTag("WEAPON"))
        {
            // Return to the starting position
            transform.position = startPosition;
        }
    }
}