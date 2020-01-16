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
}
