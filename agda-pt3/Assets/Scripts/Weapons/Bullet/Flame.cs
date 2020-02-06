using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : Bullet {

    public ArrayList objectsHit = new ArrayList ();

    void Start () {
        bulletSpeed = 0f;
        damageToGive = 1.5f;
        SetEffect(effectName);
        timeToDie = 0.3f;
    }

    void Update () {
        BulletPath (1f);
    }

    public override void BulletPath (float coefficient) {
        // transform.position += transform.up.normalized * Time.deltaTime * bulletSpeed * coefficient;
        Destroy (this.gameObject, 0.3f);
    }

    public override void OnTriggerEnter2D (Collider2D col) {
        Start ();
        if (col.gameObject.tag == "Enemy" && !objectsHit.Contains (col.gameObject)) {
            col.gameObject.GetComponent<Health> ().TakeDamage (damageToGive);
            objectsHit.Add (col.gameObject);
        }
        TriggerEffect(col);
    }
}