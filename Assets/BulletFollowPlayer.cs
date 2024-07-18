using UnityEngine;

public class BulletFollowPlayer : MonoBehaviour
{
    public float speed = 5f; // Speed at which the bullet will move
    public float shootInterval = 3f; // Time interval between each shot
    public float flipSpeed = 720f; // Speed of flipping (degrees per second)
    private Transform player; // Reference to the player's transform
    private Vector3 startPosition; // Starting position of the bullet

    [Header("Audio")]
    [SerializeField] private AudioClip shootSound; // Audio clip for the shooting sound
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Find the player by its tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Store the starting position
        startPosition = transform.position;

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Start the shooting routine
        InvokeRepeating("Shoot", 0f, shootInterval);
    }

    void Shoot()
    {
        // Play the shoot sound
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Reset the bullet's position to the starting position
        transform.position = startPosition;

        // Calculate the direction towards the player
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the bullet towards the player
            StartCoroutine(MoveBullet(direction));
        }
    }

    System.Collections.IEnumerator MoveBullet(Vector3 direction)
    {
        float elapsedTime = 0f;
        while (elapsedTime < shootInterval)
        {
            // Move the bullet towards the player
            transform.position += direction * speed * Time.deltaTime;

            // Rotate the bullet to face the direction it's moving
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Flip the bullet up and down rapidly
            transform.Rotate(Vector3.forward, flipSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet hits the player
        if (other.CompareTag("Player"))
        {
            // Add your hit logic here (e.g., reduce player's health)

            // Stop the current movement
            StopAllCoroutines();

            // Reset the bullet's position immediately (optional, for visual feedback)
            transform.position = startPosition;
        }
    }
}
