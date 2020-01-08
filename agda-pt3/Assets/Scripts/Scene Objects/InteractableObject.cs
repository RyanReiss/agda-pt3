using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour {

    // Interactable Objects are objects that can be interacted with by the player.
    // They contain an "interactionArea" which is a trigger collider in a child object.
    // That child object also possesses the script InteractionArea which handles
    // assigning the Intreact() function of this object to the player when they
    // enter the area

    public Collider2D interactionArea;
    protected virtual void Start()
    {
        interactionArea = transform.GetComponentInChildren<Collider2D>();
    }

    public abstract void Interact(); // called when a player enters the interaction area and presses "e"
}
