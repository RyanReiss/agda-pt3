using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEffect : Effect {
    
    public override void triggerEffect (GameObject bullet, Collider2D obj, float timeToDie, float k) {
        // effectPrefab = Resources.Load ("Prefabs/Bullets/Explosions") as GameObject;
        effectPrefab = null;
        if (obj.transform.name != "Player" && obj.transform.tag != "TriggersToIgnore") {
            Destroy (gameObject);
        }

        Destroy (bullet, timeToDie);
    }
}