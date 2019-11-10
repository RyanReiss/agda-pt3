using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;

    public float maxSpeed = 0.3f;

    public float minSpeed = 0.07f;
    public float friction = 4f;

    public float acceleration = 5f;

    public Transform muzzle;   //player's bullet
    GameObject bulletPrefab;
    float intervalBullet;   //the interval time of shooting a bullet

    void Start()
    {
        bulletPrefab = Resources.Load("Prefabs/Bullet/PlayerBullet") as GameObject;
    }

    // FixedUpdate is called at fixed intervals, usually every other frame
    void FixedUpdate()
    {
        Movement();
        PlayerRotate();
        PlayerShoot();
    }

    void Movement()
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

        transform.position += velocity * Time.fixedDeltaTime; // Move by velocity
    }

    void PlayerRotate()
    {
        Vector3 mouse = Input.mousePosition;    //get the mouse position 
        Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);   //player's position
        Vector3 direction = mouse - obj;  //get the direction between the mouse position and the player's position
        direction.z = 0f;    //set z axis is zero
        direction = direction.normalized;   //set the unit for direction
        transform.up = direction;
    }

    void PlayerShoot()
    {
        intervalBullet -= Time.deltaTime;
        if (intervalBullet <= 0 && Input.GetMouseButton(0)) //if player click left mouse
        {
            intervalBullet = 1.0f; //the player have a second reloading time
            Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        }

        //if (intervalBullet <= 0 && Input.GetMouseButton(1)) //if player click right mouse
        //{
        //    intervalBullet = 2.0f; //the player have a second reloading time
        //    for (int i = 0; i <= 5; i++)
        //    {
        //        Instantiate(bulletPrefab, muzzle.position, Quaternion.Euler(0, 130 + (20 * i), 0));
        //    }
            
        //}

    }
}
