using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingSun : MonoBehaviour
{
    public float radius = 20.0f;  // Radius of the circular path
    public float speed = 1.0f;   // Speed of rotation

    private Vector2 centerPoint;  // Center of the circular path
    private float angle = 0.0f;   // Current angle in radians

    void Start()
    {
        // Set the center point of the circular path
        centerPoint = transform.position;
    }

    void Update()
    {
        if (!GameManager.Instance.gameIsPaused)
        {
            // Update the angle based on time and speed
            angle += speed * Time.deltaTime;

            // Calculate the new position based on the angle and radius
            float x = centerPoint.x + radius * Mathf.Cos(angle);
            float y = centerPoint.y + radius * Mathf.Sin(angle);

            // Update the sprite's position
            transform.position = new Vector2(x, y);
        }
    }
}
