using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public Dictionary<GameObject, string> inventorySlots = new Dictionary<GameObject, string>();
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Reached here");
        foreach(Image s in transform.GetComponentsInChildren<Image>()){
            //Debug.Log("Adding + " + s.name);
            inventorySlots.Add(s.gameObject, "");
        }
    }


    public void AddItemToInventory(string str, Sprite spr){
        GameObject tempOne = null;
        string tempTwo = "";
        foreach (KeyValuePair<GameObject,string> d in inventorySlots)
        {
            if(d.Value == ""){
                Color temp = d.Key.GetComponent<Image>().color;
                temp.a = 255;
                d.Key.GetComponent<Image>().color = temp;
                d.Key.GetComponent<Image>().sprite = spr;
                tempOne = d.Key;
                tempTwo = str;
                break;
            }
        }
        if(tempOne != null){
            inventorySlots.Remove(tempOne);
            inventorySlots.Add(tempOne,tempTwo);
        }
    }

    public bool InventoryContains(string item){
        if(inventorySlots.ContainsValue(item)){
            return true;
        }
        return false;
    }
}
