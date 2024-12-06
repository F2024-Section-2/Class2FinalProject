using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [Header("Buoyancy Settings")]
    public float waterLevel = 0.0f; // Y-level of the water surface
    public float buoyancyForce = 10.0f; // Upward force when floating
    public float dragInWater = 2.0f; // Drag when in water
    public float angularDragInWater = 1.0f; // Angular drag when in water

    private Rigidbody rb;
    private bool isSubmerged;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("FloatingObject script requires a Rigidbody component!");
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        float objectHeight = transform.position.y;

        // Check if the object is submerged
        if (objectHeight < waterLevel)
        {
            isSubmerged = true;

            // Calculate depth of submersion
            float submersionDepth = waterLevel - objectHeight;

            // Apply buoyant force proportional to submersion
            Vector3 buoyantForce = Vector3.up * buoyancyForce * submersionDepth;
            rb.AddForce(buoyantForce, ForceMode.Force);

            // Add water drag
            rb.drag = dragInWater;
            rb.angularDrag = angularDragInWater;
        }
        else
        {
            isSubmerged = false;

            // Reset drag when out of water
            rb.drag = 0.0f;
            rb.angularDrag = 0.05f; // Default Rigidbody angular drag
        }
    }

    void OnDrawGizmos()
    {
        // Visualize water level in the editor
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(-1000, waterLevel, 0), new Vector3(1000, waterLevel, 0));
    }
}
