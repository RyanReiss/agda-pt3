using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration : Effect {
    private int currentPenetrationHits = 0;
    public int maxPenetrationHits;
    public override void triggerEffect (GameObject bullet, Collider2D obj, float timeToDie) {
        if(obj.transform.name != "Player" && obj.transform.tag != "TriggersToIgnore" && obj.transform.tag == "Enemy") {
            currentPenetrationHits++;
            Debug.Log("currentPenetrationHits: " + currentPenetrationHits + ", objHit = " + obj.name);
        }
        if(currentPenetrationHits >= maxPenetrationHits){
            timeToDie = 0f;
        }
        if (obj.transform.name != "Player" && obj.transform.tag != "TriggersToIgnore" && obj.transform.tag != "Enemy") {
            timeToDie = 0f;
        }
        Destroy (bullet, timeToDie);
    }
}