using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet {

    public int bulletPenetrations;

    // Start is called before the first frame update
    void Start () {
        bulletSpeed = 40f;
        damageToGive = 1f;
        effect = this.gameObject.AddComponent<NoEffect> ();
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
        if (col.gameObject.GetComponent<Health> () != null) {
            col.gameObject.GetComponent<Health> ().TakeDamage (damageToGive);
        }

        effect.triggerEffect (this.gameObject, col, timeToDie, 0f);
    }
}