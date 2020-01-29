using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenPickedUp : MonoBehaviour
{
    public string identifier;
    public bool hasBeenPickedUp;
    // Start is called before the first frame update
    void Start()
    {
        hasBeenPickedUp = false;
        GlobalGameSettings.Instance.CheckPickup(this);
    }

    public void PickUp(){
        hasBeenPickedUp = true;
        if(GlobalGameSettings.Instance.allPickupsInGame.ContainsKey(identifier)){
            GlobalGameSettings.Instance.allPickupsInGame.Remove(identifier);
            GlobalGameSettings.Instance.allPickupsInGame.Add(identifier,hasBeenPickedUp);
        } else {
            GlobalGameSettings.Instance.allPickupsInGame.Add(identifier,hasBeenPickedUp);
        }
        
    }
}
