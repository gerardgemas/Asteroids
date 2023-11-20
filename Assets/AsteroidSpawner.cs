using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] asteroidPrefabs; // Array of asteroid prefabs with different sizes
    public float spawnInterval = 1f; // Interval between spawns
    public float spawnDistance = 10f; // Distance from the screen edge to spawn asteroids

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            // Calculate a random position outside the screen
            Vector3 spawnPosition = CalculateSpawnPosition();

            // Randomly select an asteroid prefab from the array
            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

            // Instantiate the selected asteroid prefab at the spawn position
            Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 CalculateSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;

        // Determine a random direction from the screen center
        Vector2 direction = Random.insideUnitCircle.normalized;

        // Calculate the spawn position outside the screen based on direction
        spawnPosition.x = direction.x * (Screen.width / 2 + spawnDistance);
        spawnPosition.y = direction.y * (Screen.height / 2 + spawnDistance);
        spawnPosition.z = 0f; // Set z to 0 for 2D games

        return Camera.main.ScreenToWorldPoint(spawnPosition);
    }
}
