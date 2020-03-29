using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcupineQuill : Bullet
{

    void Start () {
        bulletSpeed = 4f;
        damageToGive = 1f;
        //effect = this.gameObject.AddComponent<NoEffect> ();
        SetEffect(effectName);
        timeToDie = 2.0f;
    }
    void FixedUpdate () {
        BulletPath (bulletSpeed);
        
    }
    public override void BulletPath (float coefficient) {
        
        transform.position += transform.up.normalized * Time.deltaTime * bulletSpeed * coefficient;
        //Destroy (this.gameObject, timeToDie);
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Colliding with..." + col.name);
        if (col.gameObject.GetComponent<Bullet>() || col.gameObject.GetComponent<BaseEnemyAI>())
        {
            Physics2D.IgnoreCollision(col, GetComponent<Collider2D>());
        }
        //Start ();
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<Health> ().TakeDamage (damageToGive);
        }
        //Destroy the bullet if it collides with something
        //TriggerEffect(col);
        if (col.transform.tag != "Enemy" && col.transform.tag != "TriggersToIgnore" && col.transform.tag != "Bullet") {
            Debug.Log("Colliding with..." + col.name);
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
