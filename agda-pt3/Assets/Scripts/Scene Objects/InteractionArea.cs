using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionArea : MonoBehaviour
{

    // handles assigning the Intreact() function of the parent's InteractableObject script to the player when they
    // enter the area.
    private bool added;
    public void OnTriggerEnter2D(Collider2D col){
        if(col.GetComponent<PlayerController>() != null){
            added = true;
            //Debug.Log("Player Entered Interaction Zone!");
            col.GetComponent<PlayerController>().m_currentInteractions.RemoveAllListeners();
            col.GetComponent<PlayerController>().m_currentInteractions.AddListener(transform.parent.GetComponent<InteractableObject>().Interact);
        }
    }

    public void OnTriggerStay2D(Collider2D col) {
        if(col.GetComponent<PlayerController>() != null && added == false){
            added = true;
            //Debug.Log("Player Entered Interaction Zone!");
            col.GetComponent<PlayerController>().m_currentInteractions.RemoveAllListeners();
            col.GetComponent<PlayerController>().m_currentInteractions.AddListener(transform.parent.GetComponent<InteractableObject>().Interact);
        }
    }

    public void OnTriggerExit2D(Collider2D col){
        if(col.GetComponent<PlayerController>() != null){
            added = false;
            col.GetComponent<PlayerController>().m_currentInteractions.RemoveListener(transform.parent.GetComponent<InteractableObject>().Interact);
        }
    }

    // private bool CheckListener(Collider2D col){
    //     for(int i = 0; i < col.GetComponent<PlayerController>().m_currentInteractions.GetPersistentEventCount(); i++){
    //         if(col.GetComponent<PlayerController>().m_currentInteractions.GetPersistentMethodName(i)){

    //         }){

    //         }
    //     }
    // }
}
