using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoDeSonidoMonedaOro : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isCollected = false;  // To avoid multiple trigger events

    public float amplitude = 0.1f; // The height of the floating effect
    public float frequency = 1.5f; // The speed of the floating effect
    public float collectMoveDistance = 1f; // Distance the coin moves up when collected
    public float collectMoveDuration = 0.5f; // Duration of the move up and down when collected

    private Vector3 startPosition;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isCollected)
        {
            float newY = Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            audioSource.Play();
            StartCoroutine(CollectCoin());
        }
    }

    private IEnumerator CollectCoin()
    {
        // Move up
        Vector3 upPosition = transform.position + Vector3.up * collectMoveDistance;
        yield return MoveToPosition(upPosition, collectMoveDuration / 2);

        // Move down
        Vector3 downPosition = startPosition;
        yield return MoveToPosition(downPosition, collectMoveDuration / 2);

        // Hide the coin visually
        GetComponent<SpriteRenderer>().enabled = false;

        // Destroy the coin after the sound finishes
        Destroy(gameObject, audioSource.clip.length);
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 initialPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
