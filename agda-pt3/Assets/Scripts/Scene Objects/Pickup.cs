using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : InteractableObject
{
    public override void Interact(){
        
    }

    public abstract void SetPickupValue(float value);
}
