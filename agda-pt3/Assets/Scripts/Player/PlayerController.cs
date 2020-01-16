using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float maxSpeed = 0.3f;
    public float minSpeed = 0.07f;
    public float friction = 4f;
    public float acceleration = 5f;
    public float sprintingMultiplier = 1.3f;
    // Gun Variables
    public Weapon gun; // The current gun being used by the player
    private GameObject weaponSystem;

    // Sprinting Variables
    private float energy;
    private float maxEnergy = 100f;
    private bool sprintingLock; // A lock used to stop the player from sprinting. True == able to sprint, false == not able to sprint

    // Animator variables
    private Animator anim;
    private Vector2 lastPlayerMovement;
    private bool isPlayerMoving;

    public UnityEvent m_currentInteractions;

    // Gun knockback variables
    private float currentVelocityIncrease;
    public float velocityIncreaseDecayRate;
    private bool isExceedingMaxVelocity;

    // Singleton setup
    private static PlayerController _instance;

    public static PlayerController Instance { get { return _instance; } }


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
        energy = maxEnergy;
        anim = gameObject.GetComponent<Animator>();
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
    }

    void Update(){
        gun.UpdateWeapon();
        InteractWithObjects();
    }

    void Movement()
    {
        isPlayerMoving = false;
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
        if(Input.GetKey(KeyCode.LeftShift) && energy > 0 && currentMovement != Vector3.zero && sprintingLock){
            // If the playetr is sprinting
            velocity = Vector3.ClampMagnitude(velocity, (maxSpeed * sprintingMultiplier) + currentVelocityIncrease); // Don't go faster than max speed * sprintingMultiplier
            anim.speed = sprintingMultiplier;
            energy -= 1f;
            if(energy <= 1f && sprintingLock){
                sprintingLock = false;
            }
        } else {
            // If the player Isnt sprinting
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed + currentVelocityIncrease); // Don't go faster than max speed
            anim.speed = 1;
            if(energy < 100f){
                energy += 0.5f;
            } else {
                energy = 100f;
            }

            if(!sprintingLock && energy >= 50f){
                sprintingLock = true;
            }
        }

        DecreaseTempVelocity();
        

        // If the player is moving, set isPlayerMoving = true and set the lastPlayerMovement to currentMovement
        if(currentMovement.x != 0 || currentMovement.y != 0){
            isPlayerMoving = true;
            //Debug.Log("Reached playerMoving: " + isPlayerMoving);
            lastPlayerMovement = new Vector2(currentMovement.x,currentMovement.y);
        }

        // Set Animation Value
        anim.SetBool("PlayerMoving",isPlayerMoving);


        transform.position += velocity * Time.fixedDeltaTime; // Move by velocity
    }

    void PlayerRotate()
    {
        Vector3 mouse = Input.mousePosition;    //get the mouse position 
        Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);   //player's position
        Vector3 direction = mouse - obj;  //get the direction between the mouse position and the player's position
        direction.z = 0f;    //set z axis is zero
        direction = direction.normalized;   //set the unit for direction
        // Make the player face the gun in the animator
        lastPlayerMovement = new Vector2(direction.x,direction.y);
        //Normalize, then round the direction into ordinalDirection for the animator
        direction.Normalize();
        //weaponSystem.transform.up = direction;
        gun.transform.GetChild(0).transform.up = direction;
        Vector2Int ordinalDirection = new Vector2Int(Mathf.RoundToInt(direction.x),Mathf.RoundToInt(direction.y));
        
        // Sets values for animator
        anim.SetFloat("MoveX",ordinalDirection.x);
        anim.SetFloat("MoveY",ordinalDirection.y);
        anim.SetFloat("LastMoveX",ordinalDirection.x);
        anim.SetFloat("LastMoveY",ordinalDirection.y);
        //anim.SetFloat("LastMoveX",lastPlayerMovement.x);
        //anim.SetFloat("LastMoveY",lastPlayerMovement.y);

    }

    // Called every update frame. Changes a gun depending on what button is pressed
    // Will eventually be replaced by inventory / loadout system
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
        else if (Input.GetKey("5"))
        {
            gun.gameObject.SetActive(false);
            gun = weaponSystem.GetComponentInChildren<Katana>(true);
            gun.gameObject.SetActive(true);
            //Debug.Log("Swapped Gun");
        }
    }

    // When called, interacts with any events currently subscribed to currentInteractions
    // Used to open doors, pickup items, flip switches, etc
    private void InteractWithObjects(){
        if(Input.GetKeyDown(KeyCode.E)){
            m_currentInteractions.Invoke();
        }
    }

    // Returns the current energy/sprint stamina
    public float GetEnergy(){
        return energy;
    }

    // Applies a force that pushes the player backwards when they fire a gun
    public void ApplyGunKnockback(float knockbackMultiplier){
        Vector3 knockbackDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized * -1f;
        //Debug.Log("Applied force");
        velocity += knockbackDirection * knockbackMultiplier;
        TempIncreaseVelocity(knockbackMultiplier);
    }

    private void TempIncreaseVelocity(float amount){
        currentVelocityIncrease = amount;
    }

    private void DecreaseTempVelocity(){
        if(currentVelocityIncrease > 0){
            currentVelocityIncrease -= velocityIncreaseDecayRate;
        } else {
            currentVelocityIncrease = 0;
        }
        
    }
}
