using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 velocity;

    public float maxSpeed = 0.3f;

    public float minSpeed = 0.07f;
    public float friction = 4f;

    public float acceleration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentMovement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        currentMovement *= acceleration * Time.fixedDeltaTime;
        velocity += currentMovement;
        if (currentMovement.x == 0) // If we are coasting in x direction
        {
            if (Mathf.Abs(velocity.x) < minSpeed) // if we are almost stopped
            {
                velocity.x = 0; // stop completely
            }
            else
            {
                velocity.x -= Mathf.Sign(velocity.x) * friction * Time.fixedDeltaTime; // Slow down by constant friction
            }
        }
        if (currentMovement.y == 0) // If we are coasting in y direction
        {
            if (Mathf.Abs(velocity.y) < minSpeed) // if we are almost stopped
            {
                velocity.y = 0; // stop completely
            }
            else
            {
                velocity.y -= Mathf.Sign(velocity.y) * friction * Time.fixedDeltaTime; // Slow down by constant friction
            }
        }
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed); // Don't go faster than max speed

        transform.position += velocity*Time.fixedDeltaTime; // Move by velocity
    }
}
