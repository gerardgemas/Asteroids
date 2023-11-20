using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f;
    public int size = 3; // 3 for big, 2 for medium, 1 for small

    void Start()
    {
        // Set initial movement direction
        Vector2 direction = Random.insideUnitCircle.normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject); // Destroy the current asteroid

            // Split if the size is big enough
            if (size > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject newAsteroid = Instantiate(gameObject);
                    newAsteroid.transform.localScale = transform.localScale / 2f;
                    newAsteroid.GetComponent<Asteroid>().size = size - 1;

                    // Randomize direction slightly
                    Vector2 direction = Random.insideUnitCircle.normalized;
                    newAsteroid.GetComponent<Rigidbody2D>().velocity = direction * speed;
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        // Destroy the asteroid when it is no longer visible
        Destroy(gameObject);
    }
}
