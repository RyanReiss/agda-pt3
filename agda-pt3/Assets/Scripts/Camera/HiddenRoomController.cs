using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRoomController : MonoBehaviour
{

    public List<Room> rooms;
    // Start is called before the first frame update
    void Start() {
        foreach(Room r in rooms){
            foreach(GameObject g in r.roomCovers){
                    g.SetActive(true);
                }
        }
    }

    public void ShowRoom(string name){
        foreach (Room r in rooms) {
            if(r.roomName == name){
                foreach(GameObject g in r.roomCovers){
                    g.SetActive(false);
                }
            }
        }
    }

    public void HideRoom(string name){
        foreach (Room r in rooms) {
            if(r.roomName == name){
                foreach(GameObject g in r.roomCovers){
                    g.SetActive(true);
                }
            }
        }
    }

    

    [Serializable]
    public struct Room {
        public string roomName;
        public List<GameObject> roomCovers;
    }
}
