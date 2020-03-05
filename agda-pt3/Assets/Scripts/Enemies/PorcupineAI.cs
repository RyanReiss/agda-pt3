using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcupineAI : BaseEnemyAI
{
    public float attackDistance;
    public float timeBetweenAttacks;
    public float startPreAttackTime;
    protected float attackCooldown;
    public GameObject enemyBulletPrefab;
    protected override void Start()
    {
        base.Start();
        startPreAttackTime = -timeBetweenAttacks;
        this.GetComponent<Health>().m_OnDeath.AddListener(ShootOutSpikes);
    }
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

    protected override void Movement()
    {
        if(currentState == EnemyState.TargetInSight){
            currentState = EnemyState.MovingTowardsTarget;
        }
        if(currentState == EnemyState.MovingTowardsTarget && !LineOfSight(lineOfSightDistance)){
            currentState = EnemyState.LookingForPlayer;
        }
        
        if(currentState != EnemyState.ChargingUpAttack && LineOfSight(attackDistance)  && startPreAttackTime + timeBetweenAttacks < Time.time){
            // Start Attacking
            //Debug.Log("Starting Attack...");
            currentState = EnemyState.ChargingUpAttack;
            startPreAttackTime = Time.time;
        }

        if(currentState == EnemyState.ChargingUpAttack){
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

    protected override void Attack()
    {
        this.GetComponent<Animator>().SetTrigger("ChargeUp");
        currentState = EnemyState.Attacking;
    }

    public void ShootOutSpikes(){
        currentState = EnemyState.LookingForPlayer;
        attackCooldown = Time.time;
        this.GetComponent<Animator>().SetTrigger("Attack");
        float offset = Random.Range(0f,10f);
        for(float i = offset; i < 360+offset; i+=18){
            //GameObject aBullet = Instantiate(enemyBulletPrefab,this.transform.position,Quaternion.Euler(new Vector3(0,0,i)));
            GameObject aBullet = ObjectPoolingController.Instance.GetPooledObject(enemyBulletPrefab);
            if(aBullet != null){
                //Debug.Log("Gothere");
                aBullet.transform.position = this.transform.position;
                aBullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,i));
                aBullet.transform.position = aBullet.transform.position + aBullet.transform.up*1;
                aBullet.SetActive(true);
            }
        }
        //GameObject aBullet = Instantiate(enemyBulletPrefab,this.transform.position,Quaternion.Euler(new Vector3(0,0,0)));
    }
}
