using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiClockwiseCogMovement : MonoBehaviour
{
    public float rotationSpeed = -30f;
    public float judderIntensity = 0.5f;
    public float judderFrequency = 2f;

    private Quaternion initialRotation;
    private float time;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        time += Time.deltaTime;

        // Calculate smooth rotation
        float smoothRotation = rotationSpeed * time;

        // Calculate judder effect
        float judder = Mathf.Sin(time * judderFrequency * Mathf.PI) * judderIntensity;

        // Combine smooth rotation and judder
        float finalRotation = smoothRotation + judder;

        // Apply rotation to the cog while preserving initial orientation
        transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, finalRotation);
    }
}