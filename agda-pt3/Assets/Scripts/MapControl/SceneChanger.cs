using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Vector3 location;
    public string sceneName;
    public string startingRoom;
    GameObject player;
    void Start() {
        player = GameObject.Find("Player");    
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.GetComponent<PlayerController>() != null){
            Debug.Log("Player Entered new scene!");
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            player.transform.position = location;
            if(GlobalGameSettings.Instance.allRoomsInGame.ContainsKey(startingRoom)){
                GlobalGameSettings.Instance.allRoomsInGame.Remove(startingRoom);
                GlobalGameSettings.Instance.allRoomsInGame.Add(startingRoom,false);
            }
        }
    }

}
