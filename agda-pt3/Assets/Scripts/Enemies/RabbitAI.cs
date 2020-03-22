using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : BaseEnemyAI
{
    public float attackDistance;
    public float attackSpeed;
    protected Vector3 locationToAttack;
    public float currentAttackTime;
    protected float attackCooldown;
    protected Animator anim;
    Vector3 direction = Vector3.right; // Set the default direction of the enemy (for animation)

    protected override void Start() {
        base.Start();
        anim = this.GetComponent<Animator>();
    }
    public override void UpdateEnemy()
    {
        if(currentState != EnemyState.Idle){
            // If the player isnt idle, go into handling potential movement
            Movement();
        } else {
            // If the enemy is idling...
            this.anim.SetBool("EnemyMoving",false);
            if(LineOfSight(lineOfSightDistance)){
                // If the player can be seen, stop being Idle!
                currentState = EnemyState.TargetInSight;
            }
        }
    }

    protected override void Attack()
    {
        // How the bunny attacks:
        // - Start attack animation
        // - While jumping in animation, move character quickly, almost as if they are dashing at the player.
        // - Hopefull this will look good in game
        anim.SetBool("EnemyAttacking", true);
        transform.position = Vector2.MoveTowards(transform.position, locationToAttack, attackSpeed * Time.deltaTime);
        if(transform.position == locationToAttack){
            FinishJumpAnimation();
        } else if(currentAttackTime + 0.5f < Time.time){
            // End the attack, and put it on cooldown
            currentState = EnemyState.Idle;
            attackCooldown = Time.time;
            //Debug.Log("Ending Attack...");
            anim.SetBool("EnemyAttacking", false);
        }
    }

    protected override void Movement()
    {
        if(currentState == EnemyState.TargetInSight){
            currentState = EnemyState.MovingTowardsTarget;
        }
        if(currentState == EnemyState.MovingTowardsTarget && !LineOfSight(lineOfSightDistance)){
            currentState = EnemyState.LookingForPlayer;
        }
        
        if(currentState != EnemyState.Attacking && currentState != EnemyState.WaitingForAttackToFinish && LineOfSight(attackDistance)  && attackCooldown + 1f < Time.time){
            // Start Attacking
            //Debug.Log("Starting Attack...");
            currentState = EnemyState.Attacking;
            locationToAttack = targetToAttack.position;
            currentAttackTime = Time.time;
            
        }

        if(currentState == EnemyState.Attacking){
            direction = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset) - this.transform.position; // get the direction that the enemy is moving
            Attack();
        } else if(currentState == EnemyState.MovingTowardsTarget){
            // Move towards the target
            direction = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset) - this.transform.position; // get the direction that the enemy is moving
            transform.position = Vector2.MoveTowards(transform.position, (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset), movementSpeed * Time.deltaTime); // move towards the player
            lastPositionTargetSeen = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset);
        } else if(currentState == EnemyState.LookingForPlayer){
            // If the target isnt currently in vision, move towards the last seen position
            direction = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset) - this.transform.position; // get the direction that the enemy is moving
            transform.position = Vector2.MoveTowards(transform.position, lastPositionTargetSeen, movementSpeed * Time.deltaTime); // move towards the last seen position of the target
            if(transform.position == lastPositionTargetSeen){
                //If they've reached the last seen position, sit idle
                currentState = EnemyState.Idle;
            } else if(LineOfSight(lineOfSightDistance)){
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

    public void FinishJumpAnimation(){
        if(currentState == EnemyState.WaitingForAttackToFinish){
            // Stop waiting for attack to finish and idle since attack is done
            currentState = EnemyState.Idle;
        } else if(currentState == EnemyState.Attacking){
            // Rabbit is still in leap when animation finishes... Shouldnt nessecarily reach here
            //Debug.Log("ERROR. ANIMATION FINISHED BEFORE ATTACK");
            // end attack anyways
            currentState = EnemyState.Idle;
            attackCooldown = Time.time;
        }
        anim.SetBool("EnemyAttacking", false);
    }
}
