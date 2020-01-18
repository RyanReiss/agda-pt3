using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration : Effect {
    private int currentPenetrationHits = 0;
    
    public override void triggerEffect (GameObject bullet, Collider2D obj, float timeToDie, float k) {
        effectPrefab = null;
        if (obj.transform.name != "Player" && obj.transform.tag != "TriggersToIgnore" && obj.transform.tag == "Enemy") {
            currentPenetrationHits++;
            Debug.Log ("currentPenetrationHits: " + currentPenetrationHits + ", objHit = " + obj.name);
        }
        if (currentPenetrationHits >= k) {
            timeToDie = 0f;
        }
        if (obj.transform.name != "Player" && obj.transform.tag != "TriggersToIgnore" && obj.transform.tag != "Enemy") {
            timeToDie = 0f;
        }
        Destroy (bullet, timeToDie);
    }
}