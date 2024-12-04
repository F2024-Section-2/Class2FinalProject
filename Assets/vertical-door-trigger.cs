using UnityEngine;

public class VerticalDoorTrigger : MonoBehaviour
{
    [Header("Door Components")]
    public Transform topDoorPart;
    public Transform bottomDoorPart;

    [Header("Movement Settings")]
    public float triggerDistance = 3f;
    public float openHeight = 2f;
    public float moveSpeed = 2f;

    private Vector3 topClosedPosition;
    private Vector3 bottomClosedPosition;
    private Vector3 topOpenPosition;
    private Vector3 bottomOpenPosition;
    private bool isOpen = false;

    void Start()
    {
        // Store initial positions
        topClosedPosition = topDoorPart.position;
        bottomClosedPosition = bottomDoorPart.position;

        // Calculate open positions
        topOpenPosition = topClosedPosition + Vector3.up * openHeight;
        bottomOpenPosition = bottomClosedPosition - Vector3.up * openHeight;
    }

    void Update()
    {
        // Find player distance
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Determine door state
        if (distanceToPlayer <= triggerDistance && !isOpen)
        {
            MoveDoor(true);
        }
        else if (distanceToPlayer > triggerDistance && isOpen)
        {
            MoveDoor(false);
        }
    }

    void MoveDoor(bool open)
    {
        Vector3 targetTopPosition = open ? topOpenPosition : topClosedPosition;
        Vector3 targetBottomPosition = open ? bottomOpenPosition : bottomClosedPosition;

        topDoorPart.position = Vector3.Lerp(topDoorPart.position, targetTopPosition, moveSpeed * Time.deltaTime);
        bottomDoorPart.position = Vector3.Lerp(bottomDoorPart.position, targetBottomPosition, moveSpeed * Time.deltaTime);

        // Check if door is close enough to target to consider fully open/closed
        if (Vector3.Distance(topDoorPart.position, targetTopPosition) < 0.01f)
        {
            isOpen = open;
            topDoorPart.position = targetTopPosition;
            bottomDoorPart.position = targetBottomPosition;
        }
    }
}
