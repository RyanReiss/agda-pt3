using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaBlade : Bullet
{
    // Start is called before the first frame update
    // public Transform holder;
    private List<GameObject> enemyCollisions = new List<GameObject>();
    void Start()
    {
        // bulletSpeed = 20f;
        damageToGive = 5.0f;
    }

    void FixedUpdate()
    {
    }

    public override void BulletPath(float coefficient)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy") {
            if(!enemyCollisions.Contains(col.gameObject)){
                enemyCollisions.Add(col.gameObject);
                col.gameObject.GetComponent<Health>().TakeDamage(damageToGive);
            }
        }
    }

    public void ClearListOfCollisions(){
        enemyCollisions.Clear();
    }
}