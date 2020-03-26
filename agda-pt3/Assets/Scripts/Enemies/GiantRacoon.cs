using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantRacoon : BaseEnemyAI
{
    public float attackDistance;
    public float attackSpeed;
    protected Vector3 locationToAttack;
    public float currentAttackTime;
    private float currentAttackTimeTimer;
    protected float attackCooldown;
    private float attackWaitTime = 5f;
    private float ult = 0f;
    public GameObject scream;
    public GameObject smash;
    public GameObject rabbit;
    private Health health;
    private bool attackLock = true;
    private string currentAttackName;
    private Animator anim;
    private Vector3 direction;

    protected override void Start() {
        base.Start();
        anim = this.GetComponent<Animator>();
    }
    public override void UpdateEnemy()
    {
        //Debug.Log("Current State: " + currentState.ToString());
        if (currentState != EnemyState.Idle)
        {
            // If the player isnt idle, go into handling potential movement
            Movement();
        }
        else
        {
            // If the enemy is idling...
            if (LineOfSight(lineOfSightDistance))
            {
                // If the player can be seen, stop being Idle!
                currentState = EnemyState.TargetInSight;
            }
        }
    }

    protected override void Attack()
    {
        attackLock = false;
        Instantiate(scream, this.transform.position, Quaternion.identity);
        if (currentAttackTime + 0.5f < Time.time)
        {
            // End the attack, and put it on cooldown
            currentState = EnemyState.Idle;
            attackCooldown = Time.time;
            //Debug.Log("Ending Attack...");
        }
        ult++;
        attackLock = true;
    }

    protected void screamAttack()
    {
        attackLock = false;
        currentAttackName = "scream";
        if (currentState != EnemyState.WaitingForAttackToFinish){
            // Starting scream... 
            StartScreamAttack();
        }
        //currentAttackTimeTimer = 0.5f;
        // if (currentAttackTime + 0.5f < Time.time)
        // {
        //     // End the attack, and put it on cooldown
        //     currentState = EnemyState.Idle;
        //     attackCooldown = Time.time;
        //     //Debug.Log("Ending Attack...");
        //     ult++;
        //     attackLock = true;
        //     currentAttackName = "";
        // }
    }

    protected void chargeAttack()
    {
        attackLock = false;
        currentAttackName = "charge";
        transform.position = Vector2.MoveTowards(transform.position, locationToAttack, attackSpeed * Time.deltaTime);
        if (currentAttackTime + 0.5f < Time.time)
        {
            // End the attack, and put it on cooldown
            currentState = EnemyState.Idle;
            attackCooldown = Time.time;
            //Debug.Log("Ending Attack...");
            ult++;
            attackLock = true;
            currentAttackName = "";
        }
    }

    protected void swipeAttack()
    {
        attackLock = false;
        currentAttackName = "swipe";
        if (currentState != EnemyState.WaitingForAttackToFinish){
            StartSwipeAttack();
        }
        // if (currentAttackTime + 0.5f < Time.time)
        // {
        //     // End the attack, and put it on cooldown
        //     currentState = EnemyState.Idle;
        //     attackCooldown = Time.time;
        //     //Debug.Log("Ending Attack...");
        //     ult++;
        //     attackLock = true;
        //     currentAttackName = "";
        // }
    }

    protected void summonAttack()
    {
        attackLock = false;
        currentAttackName = "summon";
        if (currentState != EnemyState.WaitingForAttackToFinish){
            Debug.Log("Spawning rabbit " + currentState.ToString());
            Instantiate(rabbit, new Vector2(Random.Range(-5, 5) + this.transform.position.x, Random.Range(-5, 5) + this.transform.position.y), Quaternion.identity);
            EnemyAIController.Instance.AddEnemiesToController();
            currentState = EnemyState.WaitingForAttackToFinish;
            currentAttackTimeTimer = 0.5f;
        }
        if (currentAttackTime + 0.5f < Time.time)
        {
            // End the attack, and put it on cooldown
            currentState = EnemyState.Idle;
            attackCooldown = Time.time;
            attackLock = true;
            //Debug.Log("Ending Attack...");
            currentAttackName = "";
        }
        
    }

    protected override void Movement()
    {
        health = GetComponent<Health>();
        if (currentState == EnemyState.TargetInSight)
        {
            currentState = EnemyState.MovingTowardsTarget;
        }
        if (currentState == EnemyState.MovingTowardsTarget && !LineOfSight(lineOfSightDistance))
        {
            currentState = EnemyState.LookingForPlayer;
        }

        if ((currentState != EnemyState.Attacking && currentState != EnemyState.WaitingForAttackToFinish) && LineOfSight(attackDistance) && attackCooldown + attackWaitTime < Time.time)
        {
            // Start Attacking
            //Debug.Log("Starting Attack...");
            direction = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset) - this.transform.position; // get the direction that the enemy is moving
            currentState = EnemyState.Attacking;
            locationToAttack = targetToAttack.position;
            currentAttackTime = Time.time;
        }

        if (currentState == EnemyState.Attacking && attackLock == true)
        {
            if (health.GetCurrentHealth() > 0.5 * health.GetMaxHealth())
            {
                if (Vector2.Distance(targetToAttack.position, this.transform.position) < 4f)
                {
                    swipeAttack();
                }
                else if (Vector2.Distance(targetToAttack.position, this.transform.position) < 7f)
                {
                    screamAttack();
                }
                else
                {
                    chargeAttack();
                }
            }
            else
            {
                attackWaitTime = 3f;
                if (Vector2.Distance(targetToAttack.position, this.transform.position) < 3f)
                {
                    screamAttack();
                }
                else if (Vector2.Distance(targetToAttack.position, this.transform.position) < 5f)
                {
                    chargeAttack();
                }
                else
                {
                    if(ult > 2)
                    {
                        summonAttack();
                    }
                }
            }
        } else if(currentState == EnemyState.Attacking && attackLock == false){
            // Current Attack has been chosen and is being executed
            switch (currentAttackName)
            {
                case "charge":
                chargeAttack();
                break;
                case "scream":
                screamAttack();
                break;
                case "swipe":
                swipeAttack();
                break;
                case "summon":
                summonAttack();
                break;
                case "":
                Debug.Log("ERROR: NO ATTACK SELECTED DURING LOCK!");
                break;
            }
        }
        else if (currentState == EnemyState.WaitingForAttackToFinish){
            if(currentAttackTime + currentAttackTimeTimer < Time.time && (currentAttackName != "scream" && currentAttackName != "swipe")){
                // End the attack, and put it on cooldown
                Debug.Log("Setting idle");
                currentState = EnemyState.Idle;
                attackCooldown = Time.time;
                //Debug.Log("Ending Attack...");
                attackLock = true;
            }
        }
        else if (currentState == EnemyState.MovingTowardsTarget)
        {
            // Move towards the target
            direction = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset) - this.transform.position; // get the direction that the enemy is moving
            transform.position = Vector2.MoveTowards(transform.position, (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset), movementSpeed * Time.deltaTime); // move towards the player
            lastPositionTargetSeen = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset);
        }
        else if (currentState == EnemyState.LookingForPlayer)
        {
            // If the target isnt currently in vision, move towards the last seen position
            direction = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset) - this.transform.position; // get the direction that the enemy is moving
            transform.position = Vector2.MoveTowards(transform.position, lastPositionTargetSeen, movementSpeed * Time.deltaTime); // move towards the last seen position of the target
            if (transform.position == lastPositionTargetSeen)
            {
                //If they've reached the last seen position, sit idle
                currentState = EnemyState.Idle;
            }
            else if (LineOfSight(lineOfSightDistance))
            {
                //If the target enters vision again, start following them
                currentState = EnemyState.TargetInSight;
            }
        }
        direction.z = 0f;
        direction = direction.normalized;
        
        Vector2Int ordinalDirection = new Vector2Int(Mathf.RoundToInt(direction.x),Mathf.RoundToInt(direction.y));
        
        // Sets values for animator
        anim.SetFloat("MoveX",ordinalDirection.x);
        anim.SetFloat("MoveY",ordinalDirection.y);
        anim.SetFloat("LastMoveX",ordinalDirection.x);
        anim.SetFloat("LastMoveY",ordinalDirection.y);
        if(currentState != EnemyState.Idle){
            this.anim.SetBool("EnemyMoving", true);
        }

    }

    public void StartScreamAttack(){
        anim.SetTrigger("ScreamAttack");
        currentState = EnemyState.WaitingForAttackToFinish;
        currentAttackTimeTimer = 0.5f;
    }

    public void InstantiateScreamAttack(){
        //Debug.Log("Spawning scream " + currentState.ToString());
        Instantiate(scream, this.transform.position, Quaternion.identity);
    }


    public void StartSwipeAttack(){
        anim.SetTrigger("SwipeAttack");
        currentState = EnemyState.WaitingForAttackToFinish;
        currentAttackTimeTimer = 0.5f;
    }

    public void InstantiateSwipeAttack(){
            Vector2 dir = targetToAttack.position - transform.position;
            dir = Vector3.ClampMagnitude(dir,4.5f);
            Debug.Log("Spawning swipe " + currentState.ToString() + " " + dir);
            Instantiate(smash, ((Vector2)transform.position + dir), Quaternion.identity);
    }

    public void StopAttack(){
        Debug.Log("stopping attack....  ");
        anim.SetTrigger("StopAttack");
        currentState = EnemyState.Idle;
        attackCooldown = Time.time;
        //Debug.Log("Ending Attack...");
        if(currentAttackName != "summon"){
            ult++;
        }
        attackLock = true;
        currentAttackName = "";
    }
}
