using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : Bullet {
    // Start is called before the first frame update
    void Start () {
        bulletSpeed = 60f;
        damageToGive = 5f;
        //effect = this.gameObject.AddComponent<NoEffect> ();
        SetEffect(effectName);
        timeToDie = 1.0f;
    }

    // Update is called once per frame
    void Update () {
        BulletPath (1f);
    }

    public override void BulletPath (float coefficient) {
        transform.position += transform.up.normalized * Time.deltaTime * bulletSpeed * coefficient;
        Destroy (this.gameObject, 2.0f);
    }

    public override void OnTriggerEnter2D (Collider2D col) {
        Start ();
        if (col.gameObject.tag == "Enemy") {
            col.gameObject.GetComponent<Health> ().TakeDamage (damageToGive);
        }
        //Destroy the bullet if it collides with something
        TriggerEffect(col);
        if (col.transform.name != "Player" && col.transform.tag != "TriggersToIgnore") {
            GameObject bulletImpact = PlayerEffectsController.Instance.GetEffect("greenBulletImpact");
            bulletImpact.transform.position = col.ClosestPoint(this.transform.position);
            bulletImpact.transform.rotation = this.transform.rotation;
            bulletImpact.transform.position += bulletImpact.transform.up*-.60f;
            bulletImpact.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}