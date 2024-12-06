using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTreeWind : MonoBehaviour
{
    [Header("Wind Settings")]
    public float swayAmplitude = 5.0f; // Maximum sway angle in degrees
    public float swaySpeed = 1.0f; // Speed of the sway

    private Vector3 initialRotation;

    void Start()
    {
        // Save the initial rotation of the tree
        initialRotation = transform.localEulerAngles;
    }

    void Update()
    {
        // Calculate the sway angle based on a sine wave
        float swayAngle = Mathf.Sin(Time.time * swaySpeed) * swayAmplitude;

        // Apply the sway by rotating the tree along the Z-axis
        transform.localEulerAngles = initialRotation + new Vector3(0, 0, swayAngle);
    }
}
