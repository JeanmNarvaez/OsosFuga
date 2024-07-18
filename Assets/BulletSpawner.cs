using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float spawnInterval = 3f; // Time interval between bullet spawns

    private void Start()
    {
        // Start spawning bullets at regular intervals
        InvokeRepeating("SpawnBullet", 0f, spawnInterval);
    }

    private void SpawnBullet()
    {
        // Instantiate a new bullet at the spawner's position and rotation
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
