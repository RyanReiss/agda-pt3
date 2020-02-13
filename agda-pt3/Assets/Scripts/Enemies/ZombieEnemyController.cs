using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieEnemyController : BaseEnemyAI
{
    // Update is called once per frame
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

    // Calculates the next move that the enemy will make before making it
    protected override void Movement(){
        //Check to make sure the player is still in vision if youre moving towards them
        if(currentState == EnemyState.MovingTowardsTarget && !LineOfSight(lineOfSightDistance)){
            currentState = EnemyState.LookingForPlayer;
        }
        if(currentState == EnemyState.TargetInSight){
            // If the target is in sight, transition towards moving towards them
            currentState = EnemyState.MovingTowardsTarget;
        }
        if(currentState == EnemyState.MovingTowardsTarget){
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
    protected override void Attack(){
        // Used when you want the enemy to have a distinct attack
        //  - i.e shoot a projectile, charge towards the target, etc.
    }
}
