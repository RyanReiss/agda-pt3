using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet {

    public int bulletPenetrations;
    //public string bulletEffect;

    // Start is called before the first frame update
    void Start () {
        bulletSpeed = 40f;
        damageToGive = 5f;
        SetEffect (effectName);
        //Debug.Log("Current Starting Effect: " + effectName);
        // effect = this.gameObject.AddComponent<NoEffect> ();
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
        TriggerEffect(col);
        if (col.transform.name != "Player" && col.transform.tag != "TriggersToIgnore") {
            GameObject bulletImpact = PlayerEffectsController.Instance.GetEffect("whiteBulletImpact");
            bulletImpact.transform.position = col.ClosestPoint(this.transform.position);
            bulletImpact.transform.rotation = this.transform.rotation;
            bulletImpact.transform.position += bulletImpact.transform.up*-.60f;
            bulletImpact.SetActive(true);
            Destroy (this.gameObject);
        }
    }
}