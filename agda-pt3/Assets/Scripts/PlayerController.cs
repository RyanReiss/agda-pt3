using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum movePositions { right, left, up, down };
    private enum Speed { still, starting, moving };
    private movePositions direction;
    private Speed animationSpeed;

    public Vector3 rawSpeed;

    private const float maxSpeed = 0.5f;

    private const float minSpeed = 0.01f;
    private const float friction = 1f;

    private const float acceleration = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        rawSpeed = Vector3.zero;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rawUp = acceleration * Input.GetAxisRaw("Vertical");
        float rawRight = acceleration * Input.GetAxisRaw("Horizontal");

        if (!(rawSpeed.y*rawUp<0))
        {
            rawSpeed.y += rawUp * Time.fixedDeltaTime;
        }
        else if (Mathf.Abs(rawSpeed.y) > minSpeed)
        {
            rawSpeed.y -= Mathf.Sign(rawSpeed.y) * friction * Time.fixedDeltaTime;
        }
        else
        {
            rawSpeed.y = 0;
        }


        if (!(rawSpeed.x*rawRight<0))
        {
            rawSpeed.x += rawRight * Time.fixedDeltaTime;
        }
        else if (Mathf.Abs(rawSpeed.x) > minSpeed)
        {
            rawSpeed.x -= Mathf.Sign(rawSpeed.x) * friction * Time.fixedDeltaTime;
        }
        else
        {
            rawSpeed.x = 0;
        }

        rawSpeed = new Vector3(Mathf.Clamp(rawSpeed.x, -maxSpeed, maxSpeed), Mathf.Clamp(rawSpeed.y, -maxSpeed, maxSpeed),0);
        transform.position += rawSpeed;
    }
}
