using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : Bullet {
    // Start is called before the first frame update

    void Start () {
        bulletSpeed = 70f;
        damageToGive = 1.667f;
        SetEffect(effectName);
        timeToDie = 1.0f;
    }

    void Update () {
        BulletPath (1f);
    }

    public override void BulletPath (float coefficient) {
        transform.position += transform.up.normalized * Time.deltaTime * bulletSpeed * coefficient;
        Destroy (this.gameObject, 0.8f);
    }

    public override void OnTriggerEnter2D (Collider2D col) {
        Start (); // Added in case OnTriggerEnter2D is called before start is called
        if (col.gameObject.GetComponent<ZombieEnemyController> ()) {
            Physics2D.IgnoreCollision (col.gameObject.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
        }
        if (col.gameObject.tag == "Enemy") {
            col.gameObject.GetComponent<Health> ().TakeDamage (damageToGive);
        }
        TriggerEffect(col);
        if (col.transform.name != "Player" && col.transform.tag != "TriggersToIgnore") {
            GameObject bulletImpact = PlayerEffectsController.Instance.GetEffect("yellowBulletImpact");
            bulletImpact.transform.position = col.ClosestPoint(this.transform.position);
            bulletImpact.transform.rotation = this.transform.rotation;
            bulletImpact.transform.position += bulletImpact.transform.up*-.60f;
            bulletImpact.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}