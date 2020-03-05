using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRoomController : MonoBehaviour
{
    public List<Room> rooms;
    public string startingRoom;
    // Start is called before the first frame update
    void Start() {
        foreach(Room r in rooms){
            if(GlobalGameSettings.Instance.allRoomsInGame.ContainsKey(r.roomName)){
                // The room has been loaded before...
                if(!GlobalGameSettings.Instance.allRoomsInGame[r.roomName]){
                    // the room has been revealed before
                    // keep room revealed
                    foreach(GameObject g in r.roomCovers){
                        g.SetActive(false);
                    }
                } else {
                    // the room hasnt been revealed before
                    // keep room hidden
                    foreach(GameObject g in r.roomCovers){
                        g.SetActive(true);
                    }
                }
            } else {
                // The room has not been loaded before...
                foreach(GameObject g in r.roomCovers){
                    g.SetActive(true);
                    if(r.roomName == startingRoom){
                        g.SetActive(false);
                    }
                }
                // Add the room to the globalRooms
                GlobalGameSettings.Instance.allRoomsInGame.Add(r.roomName,true);
            }
        }
    }

    public void ShowRoom(string name){
        foreach (Room r in rooms) {
            if(r.roomName == name){
                foreach(GameObject g in r.roomCovers){
                    g.SetActive(false);
                }
                GlobalGameSettings.Instance.allRoomsInGame.Remove(r.roomName);
                GlobalGameSettings.Instance.allRoomsInGame.Add(r.roomName,false);
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

    public void SetStartingRoom(string name){
        startingRoom = name;
    }

    

    [Serializable]
    public struct Room {
        public string roomName;
        public List<GameObject> roomCovers;
    }
}
