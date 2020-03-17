using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyAI : MonoBehaviour
{
    // An abstract class designed to be the base class layout for any Enemy AI we plan on implementing

    public enum EnemyState {
        // EnemyState is used to designate what the current State of an Enemy.
        Idle,
        TargetInSight,
        MovingTowardsTarget,
        ChargingUpAttack,
        Attacking,
        LookingForPlayer,
        ReturningToSpawnPoint,
        WaitingForAttackToFinish
    }

    public Transform targetToAttack; // Transform attached to enemy's target
    protected Vector3 lastPositionTargetSeen = Vector3.zero;
    public float movementSpeed; // Movement speed of the enemy
    public float lineOfSightDistance;
    public float damageToGive;
    public string dropTableName;
    public EnemyState currentState;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        targetToAttack = PlayerController.Instance.gameObject.transform;
        currentState = EnemyState.Idle;
    }

    // Update is called once per frame
    public abstract void UpdateEnemy();

    // Calculates the next move that the enemy will make before making it
    protected abstract void Movement();
    protected abstract void Attack();

    // Returns true if the targetToAttack is in LineOfSight
    protected bool LineOfSight(float rayCastLength){
        Vector3 start = transform.position;
        Vector3 direction = (targetToAttack.position + (Vector3)targetToAttack.GetComponent<Collider2D>().offset) - transform.position;
        //Debug.DrawRay(start,direction*rayCastLength,Color.blue,2f,false);

        RaycastHit2D[] lineOfSight = Physics2D.RaycastAll(start, direction, rayCastLength);
        int tempBuffer = 0;
        // for() loop handles line of sight, and making sure the target can be seen before moving
        // tempBuffer acts as a buffer for any colliders that might be in the way
        // but are not objects that want to block line of sight (eg. triggers)
        for (int i = 0; (i < lineOfSight.Length) && (i < 2 + tempBuffer); i++) {
            if(lineOfSight[i].transform.name == targetToAttack.name){
                //Debug.Log("Looking at Target!");
                return true;
            } else if (lineOfSight[i].transform.tag == "Bullet" || (lineOfSight[i].transform.tag == "Enemy" && lineOfSight[i].transform != this.transform)
                        || lineOfSight[i].transform.name == "InteractionArea" || lineOfSight[i].transform.tag == "TriggersToIgnore"){
                tempBuffer++;
                //Debug.Log("Enemy/Bullet in Sight!: "+ lineOfSight[i].transform.tag + ". tempBuffer = " + tempBuffer + ". lineOfSight = " + lineOfSight.Length);
            }
        }
        //Debug.Log("Not looking at target...");
        return false;
    }


    protected void OnCollisionStay2D(Collision2D collision){
        // Ignore all collisions with other enemies (Change when more enemies are added!)
      //if (collision.gameObject.GetComponent<BaseEnemyAI>())
      //{
          //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
      //}
      if (collision.gameObject.name == "Player"){
          collision.gameObject.GetComponent<Health>().TakeDamage(damageToGive);
      }
    }
}
