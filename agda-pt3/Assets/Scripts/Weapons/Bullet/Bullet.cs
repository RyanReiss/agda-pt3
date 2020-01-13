using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {
    protected float bulletSpeed;
    protected float damageToGive;
    protected Effect effect;
    protected float timeToDie;
    protected bool penned;

    public abstract void OnTriggerEnter2D (Collider2D col);
    public abstract void BulletPath (float coefficient);
}