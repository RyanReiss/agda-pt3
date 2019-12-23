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
    public Weapon gun; // The current gun being used by the player
    private GameObject weaponSystem;
    // public Melee melee;

    void Start()
    {
        weaponSystem = gameObject.transform.Find("WeaponSystem").gameObject;
        gun = weaponSystem.GetComponentInChildren<Pistol>(true);
        weaponSystem.GetComponentInChildren<AutoRifle>().gameObject.SetActive(false);
        weaponSystem.GetComponentInChildren<ShotGun>().gameObject.SetActive(false);
        weaponSystem.GetComponentInChildren<Katana>().gameObject.SetActive(false);
    }

    // FixedUpdate is called at fixed intervals, usually every other frame
    void FixedUpdate()
    {
        SelectGun();
        Movement();
        PlayerRotate();
        gun.UpdateWeapon();
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
        weaponSystem.transform.up = direction;
    }

    void SelectGun()
    {
        if (Input.GetKey("1")){
            gun.gameObject.SetActive(false);
            gun = weaponSystem.GetComponentInChildren<Pistol>(true);
            gun.gameObject.SetActive(true);
            //Debug.Log("Swapped Gun");
        }
        else if (Input.GetKey("2")){
            gun.gameObject.SetActive(false);
            gun = weaponSystem.GetComponentInChildren<AutoRifle>(true);
            gun.gameObject.SetActive(true);
            //Debug.Log("Swapped Gun");
        }
        else if (Input.GetKey("3")){
            gun.gameObject.SetActive(false);
            gun = weaponSystem.GetComponentInChildren<ShotGun>(true);
            gun.gameObject.SetActive(true);
            //Debug.Log("Swapped Gun");
        }
        else if (Input.GetKey("4"))
        {
            gun.gameObject.SetActive(false);
            gun = weaponSystem.GetComponentInChildren<FlameThrower>(true);
            gun.gameObject.SetActive(true);
            //Debug.Log("Swapped Gun");
        }
    }
}
