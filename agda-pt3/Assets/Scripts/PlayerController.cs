using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 rawVelocity;

    public float maxSpeed = 0.3f;

    public float minSpeed = 0.07f;
    public float friction = 4f;

    public float acceleration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rawVelocity = Vector3.zero;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rawMovement = acceleration * Time.fixedDeltaTime * new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        rawVelocity += rawMovement;
        if (rawMovement.x == 0)
        {
            if (Mathf.Abs(rawVelocity.x) < minSpeed)
            {
                rawVelocity.x = 0;
            }
            else
            {
                rawVelocity.x -= Mathf.Sign(rawVelocity.x) * friction * Time.fixedDeltaTime;
            }
        }
        if (rawMovement.y == 0)
        {
            if (Mathf.Abs(rawVelocity.y) < minSpeed)
            {
                rawVelocity.y = 0;
            }
            else
            {
                rawVelocity.y -= Mathf.Sign(rawVelocity.y) * friction * Time.fixedDeltaTime;
            }
        }
        rawVelocity = Vector3.ClampMagnitude(rawVelocity, maxSpeed);

        transform.position += rawVelocity;
    }
}
