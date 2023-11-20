using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool forwardInput;
    private bool leftInput;
    private bool rightInput;
    private bool shootInput;
    public float speed = 10;
    public GameObject bulletPrefab; // Reference to your bullet prefab
    public Transform firePoint; // Point where the bullet will be generated
    public float bulletSpeed = 10.0f; // Adjust this value for bullet speed
    public float shootCooldown = 0.5f; // Adjust this value for the cooldown time
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent < Rigidbody2D>();
    }

    void Update()
    {
        forwardInput = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        leftInput = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        rightInput = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        shootInput = Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space);

        
    }

    void FixedUpdate()
    {
        if (forwardInput)
        {
            rb.AddForce((Vector2)transform.up * speed);
        }
        if (leftInput)
        {
            rb.AddTorque(1);
        }
        if (rightInput)
        {
            rb.AddTorque(-1);
        }
        if (shootInput && cooldownTimer <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            cooldownTimer = shootCooldown;
        }


        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        CheckScreenBoundaries();
    }
    void CheckScreenBoundaries()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPos.x > 1.0f) // Ship crosses right boundary
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, viewportPos.y, 0.0f));
        }
        else if (viewportPos.x < 0.0f) // Ship crosses left boundary
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, viewportPos.y, 0.0f));
        }

        if (viewportPos.y > 1.0f) // Ship crosses top boundary
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewportPos.x, 0.0f, 0.0f));
        }
        else if (viewportPos.y < 0.0f) // Ship crosses bottom boundary
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewportPos.x, 1.0f, 0.0f));
        }
    }


}