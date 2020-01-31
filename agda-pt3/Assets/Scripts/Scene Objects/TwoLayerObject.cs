using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoLayerObject : MonoBehaviour
{
    public string frontLayerName;
    public string backLayerName;
    private int frontLayer;
    private int backLayer;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        frontLayer = SortingLayer.GetLayerValueFromName(frontLayerName);
        backLayer = SortingLayer.GetLayerValueFromName(backLayerName);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.y <= this.transform.position.y && transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName == frontLayerName){
            // The player is in front of the object
            Debug.Log("Swapped to back");
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = backLayerName;
        } else if(player.position.y > this.transform.position.y && transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName == backLayerName){
            Debug.Log("Swapped to front");
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = frontLayerName;
        }
    }
}
