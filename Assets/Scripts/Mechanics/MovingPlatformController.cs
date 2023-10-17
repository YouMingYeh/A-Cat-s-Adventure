using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    private Vector3 StartingPoint;
    private Vector3 EndingPoint;
    public PatrolPath PatrolPath;
    public float platformSpeed = 2.0f;

    private Rigidbody2D rb;
    private Vector3 currentTarget;
    private bool movingToEnd = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        StartingPoint = PatrolPath.startPosition;
        EndingPoint = PatrolPath.endPosition;
        currentTarget = EndingPoint;
    }

    private void Update()
    {
        // Calculate the oscillation factor based on time and speed
        float oscillationFactor = Mathf.Sin(Time.time * platformSpeed);

        // Calculate the direction based on the oscillation factor
        Vector3 direction = (currentTarget).normalized;

        // Apply the oscillation factor to the direction
        direction *= oscillationFactor;

        // Set the platform's velocity
        rb.velocity = direction;

        // Check if the platform has reached the target point and switch direction
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            if (movingToEnd)
            {
                currentTarget = StartingPoint;
            }
            else
            {
                currentTarget = EndingPoint;
            }
            movingToEnd = !movingToEnd;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Rigidbody2D PlayerRb= collision.GetComponent<Rigidbody2D>();
            PlayerRb.velocity = rb.velocity;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Rigidbody2D PlayerRb = collision.GetComponent<Rigidbody2D>();
            PlayerRb.velocity = Vector3.zero;
        }
    }
}
