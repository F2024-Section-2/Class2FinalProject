using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speed = 58f; // Speed of the ship
    public float waveAmplitude = 1f; // Amplitude of the wave movement
    public float waveFrequency = 1f; // Frequency of the wave movement
    public Transform oceanPlane; // Reference to the ocean surface (optional)

    private Vector3 startPosition;

    void Start()
    {
        // Record the initial position of the ship
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the ship forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Simulate wave-like movement
        float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        transform.position = new Vector3(
            transform.position.x,
            startPosition.y + waveOffset,
            transform.position.z
        );

        // Optional: Align the ship to the ocean plane's rotation
        if (oceanPlane != null)
        {
            transform.rotation = Quaternion.Euler(oceanPlane.eulerAngles.x, transform.eulerAngles.y, oceanPlane.eulerAngles.z);
        }
    }
}
