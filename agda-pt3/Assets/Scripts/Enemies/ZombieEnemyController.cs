using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieEnemyController : MonoBehaviour
{

    public Transform playerToFollow;
    public float moveSpeed;
    private float stoppingDistance; //Distance the enemy will stop moving towards the player
    private Rigidbody2D rb;
    public Vector3 lastPositionTargetSeen = Vector3.zero;
    public UnityEvent m_currentInteractions;
    public float damageToGive;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerToFollow = PlayerController.Instance.gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    // MoveTowardsPlayer() is called once every update frame, and handles all movement that
    // the ZombieEnemyController does when moving towards the player. It will only start
    // to move towards the player when it has a line of sight to the player
    void MoveTowardsPlayer() {
        Vector3 start = transform.position;
        Vector3 direction = playerToFollow.position - transform.position;

        float distance = 100f;
        //Debug.DrawRay(start,direction*distance,Color.blue,2f,false);

        RaycastHit2D[] lineOfSight = Physics2D.RaycastAll(start, direction, distance);

        bool playerIsInVision = false; // true when the player is in line of sight
        int tempBuffer = 0;
        // for() loop handles line of sight, and making sure the player can be seen before moving
        // tempBuffer acts as a buffer for any colliders that might be in the way
        // but are not objects that want to block line of sight (eg. triggers)
        for (int i = 0; (i < lineOfSight.Length) && (i < 2 + tempBuffer); i++) {
            if(lineOfSight[i].transform.name == playerToFollow.name){
                //Debug.Log("Looking at Player!");
                playerIsInVision = true;
            } else if (lineOfSight[i].transform.tag == "Bullet" || (lineOfSight[i].transform.tag == "Enemy" && lineOfSight[i].transform != this.transform)
                        || lineOfSight[i].transform.name == "InteractionArea"){
                //Debug.Log("Enemy/Bullet in Sight!: "+ lineOfSight[i].transform.tag);
                tempBuffer++;
            }
        }

        if(playerIsInVision){
            if(Vector2.Distance(transform.position, playerToFollow.position) > stoppingDistance){
                    transform.position = Vector2.MoveTowards(transform.position, playerToFollow.position, moveSpeed * Time.deltaTime); // move towards the player
            }
            lastPositionTargetSeen = playerToFollow.position;
        } else if(lastPositionTargetSeen != Vector3.zero){
            if(Vector2.Distance(transform.position, playerToFollow.position) > stoppingDistance){
                    transform.position = Vector2.MoveTowards(transform.position, lastPositionTargetSeen, moveSpeed * Time.deltaTime); //
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        // Ignore all collisions with other enemies (Change when more enemies are added!)
      if (collision.gameObject.GetComponent<ZombieEnemyController>())
      {
          Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
      }
      if (collision.gameObject.name == "Player"){
          collision.gameObject.GetComponent<Health>().TakeDamage(damageToGive);
      }
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<InteractionArea>() && collision.transform.parent.gameObject.GetComponent<Door>() && this.lastPositionTargetSeen != Vector3.zero){
            if(!collision.transform.parent.GetComponent<Door>().GetState()){
                collision.transform.parent.GetComponent<InteractableObject>().Interact();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<InteractionArea>() && collision.transform.parent.gameObject.GetComponent<Door>() && this.lastPositionTargetSeen != Vector3.zero){
            if(!collision.transform.parent.GetComponent<Door>().GetState()){
                collision.transform.parent.GetComponent<InteractableObject>().Interact();
            }
        }
    }
}
