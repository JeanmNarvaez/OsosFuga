using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float amplitude = 0.1f; // Further reduced height of the floating effect
    public float frequency = 1.5f; // Adjusted speed of the floating effect

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float newY = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z);
    }
}
