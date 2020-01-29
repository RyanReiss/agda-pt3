using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushback : Effect {
    public override void triggerEffect (GameObject bullet, Collider2D obj, float timeToDie, float k) {

        if (obj.transform.name != "Player" && obj.transform.tag != "TriggersToIgnore") {
            if (obj.transform.tag != "Map")
                obj.transform.position += bullet.transform.up.normalized * Time.deltaTime * k;
            // obj.position += obj.up.normalized * Time.deltaTime * k;
            timeToDie = 0f;
        }

        Destroy (bullet, timeToDie);
    }
}