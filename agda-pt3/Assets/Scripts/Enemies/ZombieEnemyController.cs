using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemyController : MonoBehaviour
{

    public Transform playerToFollow;
    public float moveSpeed;
    private float stoppingDistance;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer() {
        Vector3 start = transform.position;
        Vector3 direction = playerToFollow.position - transform.position;

        float distance = 100f;
        Debug.DrawRay(start,direction*distance,Color.blue,2f,false);

        RaycastHit2D[] lineOfSight = Physics2D.RaycastAll(start, direction, distance);

        for (int i = 0; (i < lineOfSight.Length) && (i < 2); i++)
        {
            if(lineOfSight[i].transform.name == "Player"){
                //Debug.Log("Looking at Player!");
                if(Vector2.Distance(transform.position, playerToFollow.position) > stoppingDistance){
                    transform.position = Vector2.MoveTowards(transform.position, playerToFollow.position, moveSpeed * Time.deltaTime);
                }
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.GetComponent<ZombieEnemyController>())
      {
          Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
      }
  }
}
