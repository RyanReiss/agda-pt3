using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed;
    public float damageToGive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletPath();
    }

    void BulletPath()
    {
        transform.position += transform.up.normalized * Time.deltaTime * bulletSpeed;
        Destroy(this.gameObject, 5.0f);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.GetComponent<Health>() != null){
            col.gameObject.GetComponent<Health>().TakeDamage(damageToGive);
        }
        //Destroy the bullet if it collides with something
        Destroy(gameObject);
    }
}
