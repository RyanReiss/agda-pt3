using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float damage;
    public float fadeTime;

    void Start()
    {
        Destroy(this.gameObject, fadeTime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
