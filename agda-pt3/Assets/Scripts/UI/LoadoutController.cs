using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutController : MonoBehaviour
{
    // Class attached to loadout UI that controls swapping and inspecting weapons
    public Image primaryWeaponSlot;
    public Image secondaryWeaponSlot;
    public Image backpackSlot;
    public PlayerController player;
    //Moving backpack controls
    private int lengthOfBackpack;
    private int currentBackpackIndex;
    public GameObject hiddenGuns;
    // Singleton setup
    private static LoadoutController _instance;

    public static LoadoutController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(this.transform.parent.gameObject);
    }


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() as PlayerController;
        lengthOfBackpack = backpackSlot.transform.childCount-1;
        currentBackpackIndex = 0;
        foreach (GameObject g in backpackSlot.transform.GetComponentsInChildren<GameObject>())
        {
            g.SetActive(false);
        }
        backpackSlot.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SwapWeaponsInBackPack(GameObject weapon, bool primaryOrSecondary){
        //primaryOrSeconday = true: primary gun
        //primaryOrSeconday = false: secondary gun
        if(primaryOrSecondary){
            //primary
            //backpackWeapons.Add(player.primaryWeaponHolder.transform.GetChild(0));
            //backpackWeapons.Remove(player.weaponBackpack.GetComponentInChildren(weapon.GetComponent<Weapon>().GetType()));
            player.primaryWeaponHolder.transform.GetChild(0).parent = player.weaponBackpack.transform;
            player.weaponBackpack.GetComponentInChildren(weapon.GetComponent<Weapon>().GetType()).transform.parent = player.primaryWeaponHolder.transform;
        } else {
            //secondary
            //backpackWeapons.Add(player.secondaryWeaponHolder.transform.GetChild(0));
            //backpackWeapons.Remove(player.weaponBackpack.GetComponentInChildren(weapon.GetComponent<Weapon>().GetType()));
            player.secondaryWeaponHolder.transform.GetChild(0).parent = player.weaponBackpack.transform;
            player.weaponBackpack.GetComponentInChildren(weapon.GetComponent<Weapon>().GetType()).transform.parent = player.secondaryWeaponHolder.transform;
        }
    }

    public void SwapWeapons(GameObject first, GameObject second){
        Transform temp = first.transform.parent;
        first.transform.parent = second.transform.parent;
        second.transform.parent = temp;
    }

    public void SwapPrimaryAndSecondary(){
        Transform temp = player.secondaryWeaponHolder.transform.GetChild(0);
        player.primaryWeaponHolder.transform.GetChild(0).parent = player.secondaryWeaponHolder.transform;
        temp.parent = player.primaryWeaponHolder.transform;
    }

    public void MoveBackPackRight(){
        if(lengthOfBackpack != backpackSlot.transform.childCount-1){
            lengthOfBackpack = backpackSlot.transform.childCount-1;
        }
        currentBackpackIndex++;
        if(currentBackpackIndex > lengthOfBackpack){
            currentBackpackIndex = 0;
        }
        SetBackPackDisplay(currentBackpackIndex);
    }

    public void MoveBackPackLeft(){
        if(lengthOfBackpack != backpackSlot.transform.childCount-1){
            lengthOfBackpack = backpackSlot.transform.childCount-1;
        }
        currentBackpackIndex--;
        if(currentBackpackIndex < 0){
            currentBackpackIndex = backpackSlot.transform.childCount-1;
        }
        SetBackPackDisplay(currentBackpackIndex);
    }

    private void SetBackPackDisplay(int index){
        Debug.Log("Setting Backpack Display: " + index);
        for(int i = 0; i <= lengthOfBackpack; i++){
            if(i == index){
                backpackSlot.transform.GetChild(i).gameObject.SetActive(true);
            } else {
                backpackSlot.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
    }

    public void AddGunToDisplay(string gun){
        GameObject temp = hiddenGuns.transform.Find(gun).gameObject;
        if(temp != null){
            temp.transform.parent = backpackSlot.transform;
            lengthOfBackpack = backpackSlot.transform.childCount-1;
        }
    }
}
