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
    public override void UpdateEnemy()
    {
        if(currentState != EnemyState.Idle){
            // If the player isnt idle, go into handling potential movement
            Movement();
        } else {
            // If the enemy is idling...
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
        transform.position = Vector2.MoveTowards(transform.position, locationToAttack, attackSpeed * Time.deltaTime);
        if(currentAttackTime + 0.5f < Time.time){
            // End the attack, and put it on cooldown
            currentState = EnemyState.Idle;
            attackCooldown = Time.time;
            //Debug.Log("Ending Attack...");
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
        
        if(currentState != EnemyState.Attacking && LineOfSight(attackDistance)  && attackCooldown + 1f < Time.time){
            // Start Attacking
            //Debug.Log("Starting Attack...");
            currentState = EnemyState.Attacking;
            locationToAttack = targetToAttack.position;
            currentAttackTime = Time.time;
        }

        if(currentState == EnemyState.Attacking){
            Attack();
        } else if(currentState == EnemyState.MovingTowardsTarget){
            // Move towards the target
            transform.position = Vector2.MoveTowards(transform.position, (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset), movementSpeed * Time.deltaTime); // move towards the player
            lastPositionTargetSeen = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset);
        } else if(currentState == EnemyState.LookingForPlayer){
            // If the target isnt currently in vision, move towards the last seen position
            transform.position = Vector2.MoveTowards(transform.position, lastPositionTargetSeen, movementSpeed * Time.deltaTime); // move towards the last seen position of the target
            if(transform.position == lastPositionTargetSeen){
                //If they've reached the last seen position, sit idle
                currentState = EnemyState.Idle;
            } else if(LineOfSight(lineOfSightDistance)){
                //If the target enters vision again, start following them
                currentState = EnemyState.TargetInSight;
            }
        }
        
    }
}
