using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform tracked;
    public float minSpeed = 0.1f;
    public float minDisplacement = 0.1f;
    public float elasticSpeed = 0.4f;

    public float elasticRamp = 1.4f;
    void Start()
    {
        tracked = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 displacement = tracked.transform.position-transform.position;
        displacement.z=0;
        if (displacement.magnitude > minSpeed){
            Vector3 velocity = Vector3.Normalize(displacement)*Mathf.Pow(displacement.magnitude+minSpeed, elasticRamp)*
            Mathf.Pow(Time.fixedDeltaTime*elasticSpeed, 1/elasticRamp);
            transform.position += velocity;
        }
    }
}
