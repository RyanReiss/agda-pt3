using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{

    // handles assigning the Intreact() function of the parent's InteractableObject script to the player when they
    // enter the area.

    public void OnTriggerEnter2D(Collider2D col){
        if(col.GetComponent<PlayerController>() != null){
            Debug.Log("Player Entered Interaction Zone!");
            col.GetComponent<PlayerController>().m_currentInteractions.AddListener(transform.parent.GetComponent<InteractableObject>().Interact);
        }

        if(col.GetComponent<ZombieEnemyController>() && col.GetComponent<ZombieEnemyController>().lastPositionTargetSeen != Vector3.zero){
            if(!transform.parent.GetComponent<Door>().GetState()){
                transform.parent.GetComponent<InteractableObject>().Interact();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D col) {
        if(col.GetComponent<ZombieEnemyController>() && col.GetComponent<ZombieEnemyController>().lastPositionTargetSeen != Vector3.zero){
            if(!transform.parent.GetComponent<Door>().GetState()){
                transform.parent.GetComponent<InteractableObject>().Interact();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D col){
        if(col.GetComponent<PlayerController>() != null){
            col.GetComponent<PlayerController>().m_currentInteractions.RemoveListener(transform.parent.GetComponent<InteractableObject>().Interact);
        }
    }
}
