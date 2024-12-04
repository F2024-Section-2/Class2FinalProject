using UnityEngine;

public class dogfollow : MonoBehaviour
{
    // The player (VR camera or the player character)
    public Transform player;

    // The follow distance (adjust to your preference)
    public float followDistance = 2.0f;

    // Speed at which the dog follows the player
    public float followSpeed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // Ensure the dog stays at a certain distance behind the player
        if (player != null)
        {
            // Calculate the desired position of the dog
            Vector3 targetPosition = player.position - player.forward * followDistance;
            targetPosition.y = player.position.y; // Keep the dog's height the same as the player's

            // Move the dog towards the target position with smoothing
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Optionally, you can make the dog rotate towards the player
            Vector3 direction = player.position - transform.position;
            if (direction.sqrMagnitude > 0.01f) // Avoid rotating if very close to the player
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * followSpeed);
            }
        }
    }
}
