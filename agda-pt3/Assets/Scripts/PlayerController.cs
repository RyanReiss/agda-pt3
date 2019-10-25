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

    public float maxSpeed = 0.5f;

    public float minSpeed = 0.03f;
    public float friction = 1.5f;

    public float acceleration = 0.8f;

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

        if (rawUp != 0f && !(rawSpeed.y*rawUp<0))
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


        if (rawRight != 0f && !(rawSpeed.x*rawRight<0))
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
