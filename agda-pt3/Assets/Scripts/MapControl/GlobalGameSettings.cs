using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameSettings : MonoBehaviour
{
    // Class designed to store/handle information that is needed across multiple scenes
    // i.e. exlpored rooms, items picked up, locked doors, etc.
    private static GlobalGameSettings _inst; // singleton
    public static GlobalGameSettings Instance { get { return _inst; } } // singleton

    // allRoomsInGame stores every single room created by HiddenRoomController, and stores whether they should be hidden (true) or not (false)
    public Dictionary<string,bool> allRoomsInGame = new Dictionary<string, bool>();
    // allPickups <string,bool> = <pickupIdentifier,pickedUpOrNot>. pickedUpOrNot is true when it has been picked up, false when it has not been picked up
    public Dictionary<string,bool> allPickupsInGame = new Dictionary<string, bool>();



    private void Awake()
    {
        if (_inst != null && _inst != this)
        {
            Destroy(this.gameObject);
        } else {
            _inst = this;
        }
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When this script is called, it adds all item pickups to a dictionary
    // 
    public void AddAllObjectsForDeletion(){
        foreach (DestroyWhenPickedUp d in Object.FindObjectsOfType<DestroyWhenPickedUp>())
        {
            if(allPickupsInGame.ContainsKey(d.identifier)){
                // If the item already exists...
                if(allPickupsInGame[d.identifier]){
                    // If the item has been picked up, remove it from the scene
                    GameObject.Destroy(d.gameObject);
                }
            }
        }
    }

    public void AddObjectForDeletion(DestroyWhenPickedUp d){

    }

    public void CheckPickup(DestroyWhenPickedUp d){
        if(allPickupsInGame.ContainsKey(d.identifier) && allPickupsInGame[d.identifier]){
            GameObject.Destroy(d.gameObject);
        } else if(!allPickupsInGame.ContainsKey(d.identifier)){
            //If for some reason the item hasnt been added already... add it
            allPickupsInGame.Add(d.identifier,d.hasBeenPickedUp);
        }
    }
}
