using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Vector3 currentRespawnPoint;
    public string sceneToRespawnIn;



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
        currentRespawnPoint = Vector3.zero;
        sceneToRespawnIn = "TilemapTestScene";
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

    public void SetNewRespawnPoint(string currentScene){
        Debug.Log("Setting respawn point...: " + currentScene);
        switch (currentScene)
        {
            case "FirstBossArea":
                currentRespawnPoint = new Vector3(15f,0f,0f);
                sceneToRespawnIn = currentScene;
                break;
            case "ForestIntro":
                currentRespawnPoint = new Vector3(-50f,10f,0f);
                sceneToRespawnIn = currentScene;
                break;
            case "TilemapTestScene":
                currentRespawnPoint = new Vector3(3.84f,-2.87f,0f);
                sceneToRespawnIn = currentScene;
                break;
            case "InsideArea2":
                currentRespawnPoint = new Vector3(-32.51f, -23.45f,0f);
                sceneToRespawnIn = currentScene;
                break;
            default:
                break;
        }
    }

    public void SetPlayerSizeBasedOnScene(string currentScene){
        switch (currentScene)
        {
            case "House-f1-CutsceneTest":
                PlayerController.Instance.transform.localScale = new Vector3(3f,3f,1f);
                break;
            case "House-f1":
                PlayerController.Instance.transform.localScale = new Vector3(3f,3f,1f);
                break;
            case "House-f2":
                PlayerController.Instance.transform.localScale = new Vector3(3f,3f,1f);
                break;
            case "House-f2-pre":
                PlayerController.Instance.transform.localScale = new Vector3(3f,3f,1f);
                break;
            case "Barn":
                PlayerController.Instance.transform.localScale = new Vector3(3f,3f,1f);
                break;
            case "Barn2":
                PlayerController.Instance.transform.localScale = new Vector3(3f,3f,1f);
                break;
            case "Barn3":
                PlayerController.Instance.transform.localScale = new Vector3(3f,3f,1f);
                break;
            default:
                PlayerController.Instance.transform.localScale = new Vector3(1.5f,1.5f,1f);
                break;
        }
    }

    
}
