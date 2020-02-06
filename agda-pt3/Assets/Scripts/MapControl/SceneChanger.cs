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
    private float msUntilUnlocked;
    private float waitTime = 0.02f;

    void Start() {
        player = GameObject.Find("Player");  
        msUntilUnlocked = 0;
    }

    void Update(){
        msUntilUnlocked++;
    }

    public void OnTriggerEnter2D(Collider2D col){
        //Debug.Log(msUntilUnlocked);
        if(col.GetComponent<PlayerController>() != null && msUntilUnlocked/1000f > waitTime){
            Debug.Log("Player Entered new scene!");
            StartCoroutine(ScreenFadeController.Instance.FadeToNewLevel(sceneName, location));
            //player.transform.position = location;
            //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            Debug.Log("Loaded Scene: " + sceneName);
            if(GlobalGameSettings.Instance.allRoomsInGame.ContainsKey(startingRoom)){
                GlobalGameSettings.Instance.allRoomsInGame.Remove(startingRoom);
                GlobalGameSettings.Instance.allRoomsInGame.Add(startingRoom,false);
            }
        }
    }

}
