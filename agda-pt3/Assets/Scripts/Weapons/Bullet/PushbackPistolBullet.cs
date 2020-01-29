using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushbackPistolBullet : Bullet {

    public int bulletPenetrations;

    // Start is called before the first frame update
    void Start () {
        bulletSpeed = 40f;
        damageToGive = 1f;
        effect = this.gameObject.AddComponent<Pushback> ();
        // effect.GetComponent<Penetration>().maxPenetrationHits = bulletPenetrations; // Default
        timeToDie = 1.0f;
    }

    // Update is called once per frame
    void Update () {
        BulletPath (1f);
    }

    public override void BulletPath (float coefficient) {
        transform.position += transform.up.normalized * Time.deltaTime * bulletSpeed * coefficient;
        Destroy (this.gameObject, timeToDie);
    }

    public override void OnTriggerEnter2D (Collider2D col) {
        if (col.gameObject.tag == "Enemy") {
            col.gameObject.GetComponent<Health> ().TakeDamage (damageToGive);
        }

        effect.triggerEffect (this.gameObject, col, timeToDie, 80f);
    }
}