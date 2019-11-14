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
        if(Vector2.Distance(transform.position, playerToFollow.position) > stoppingDistance){
            transform.position = Vector2.MoveTowards(transform.position, playerToFollow.position, moveSpeed * Time.deltaTime);
        }
    }
}
