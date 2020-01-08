using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera that tracks an object in 2d 

    private Transform tracked;
    public enum Modes { Elastic, Fixed, Tracking, Sticky }; // The different modes that the camera can be in
    public Modes mode;
    public float minSpeed = 0.4f;
    public float minDisplacement = 0.1f;
    public float elasticSpeed = 0.3f;
    public float elasticRamp = 1.4f;

    public Vector3 stickyPosition;
    public float stickyRadius = 10f;
    public float stickySpeed = 0.9f;
    private float sticyMinDisplacement = 0.15f;
    void Start()
    {
        stickyPosition = transform.position;

        tracked = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (mode)
        {
            case Modes.Elastic:
                Vector3 displacement = tracked.transform.position - transform.position;
                displacement.z = 0;
                if (displacement.magnitude > minSpeed)
                {
                    Vector3 velocity = Vector3.Normalize(displacement) * Mathf.Pow(displacement.magnitude + minSpeed, elasticRamp) *
                    Mathf.Pow(Time.fixedDeltaTime * elasticSpeed, 1 / elasticRamp);
                    transform.position += velocity;
                }
                break;
            case Modes.Tracking:
                Vector3 trackedPosition = tracked.position;
                trackedPosition.z = transform.position.z;
                // Camera height doesn't follow tracked object, because object is moving in 2d
                transform.position = trackedPosition;
                break;
            case Modes.Sticky:
                Vector3 tPos = tracked.position;
                tPos.z = transform.position.z;
                if ((tPos - stickyPosition).magnitude > stickyRadius)
                {
                    stickyPosition = tracked.position + Vector3.Normalize(tracked.position - transform.position) * stickyRadius * 2;
                    stickyPosition.z = transform.position.z;
                }
                if ((transform.position - stickyPosition).magnitude > minDisplacement)
                {
                    transform.position += Vector3.Normalize(stickyPosition - transform.position) * Time.fixedDeltaTime * stickySpeed;
                }
                else
                {
                    transform.position = stickyPosition;
                }
                break;
            case Modes.Fixed:
                break;
        }
    }
}