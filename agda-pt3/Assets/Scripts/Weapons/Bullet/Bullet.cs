using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {
    protected float bulletSpeed;
    protected float damageToGive;
    protected Effect effect;
    protected float timeToDie;
    protected GameObject effectPrefab;
    public string effectName;

    public abstract void OnTriggerEnter2D (Collider2D col);
    public abstract void BulletPath (float coefficient);
    public void SetEffect (string effectKey) {
        Debug.Log ("effectKey in bullet: " + effectKey);
        effectName = effectKey;
        if(this.gameObject.GetComponent<Effect>()){
            Destroy(this.gameObject.GetComponent<Effect>());
        }
        switch (effectKey) {
            case "Explosion":
                Debug.Log ("Explosion!!!");
                effect = this.gameObject.AddComponent<Explosion> ();
                break;
            case "Penetration":
                Debug.Log ("Penetration!!!");
                effect = this.gameObject.AddComponent<Penetration> ();
                break;
            case "Pushback":
                Debug.Log ("Pushback!!!");
                effect = this.gameObject.AddComponent<Pushback> ();
                break;
            case "NoEffect":
                Debug.Log ("NoEffect!!!");
                effect = this.gameObject.AddComponent<NoEffect> ();
                break;
            default:
                Debug.Log ("Wrong Effect!!!");
                effect = this.gameObject.AddComponent<NoEffect> ();
                break;
        }
    }

    public void TriggerEffect(Collider2D col){
        if(effect.gameObject.GetComponent<Explosion>()){
            effect.triggerEffect(this.gameObject, col, 3f);
        } else {
            effect.triggerEffect (this.gameObject, col, 0f);
        }
    }
}