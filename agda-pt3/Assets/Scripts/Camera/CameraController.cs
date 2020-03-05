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
    
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        stickyPosition = transform.position;

        tracked = GameObject.Find("Player").transform;
        transform.position = new Vector3(tracked.position.x,tracked.position.y,transform.position.z);
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

    public void SetCameraPosition(Vector3 pos){
        Vector3 cameraPos = new Vector3(pos.x,pos.y,-10f);
        stickyPosition = cameraPos;
        transform.position = cameraPos;
        
    }

    public void MoveCameraTowardsPosition(Vector3 pos){
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(pos.x,pos.y,-10f), 0.1f);
    }

    public void SetCameraMode(CameraController.Modes modeToSwapTo){
        mode = modeToSwapTo;
    }
}