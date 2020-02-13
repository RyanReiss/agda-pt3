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
    public Text primaryWeaponText;
    public Text secondaryWeaponText;
    public Text backpackWeaponText;
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
        // foreach (Image g in backpackSlot.transform.GetComponentsInChildren<Image>())
        // {
        //     g.gameObject.SetActive(false);
        // }
        if(backpackSlot.transform.childCount >= 1)
            backpackSlot.transform.GetChild(0).gameObject.SetActive(true);
        ReloadLoadoutText();
    }
    
    private void LateUpdate() {
        ReloadLoadoutText();
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
        //Debug.Log("Setting Backpack Display: " + index);
        for(int i = 0; i <= lengthOfBackpack; i++){
            if(i == index){
                backpackSlot.transform.GetChild(i).gameObject.SetActive(true);
            } else {
                backpackSlot.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void AddGunToDisplay(GameObject gun, string gunName){
        GameObject temp2 = Resources.Load("Prefabs/UI/LoadoutImages/" + gunName) as GameObject;
        GameObject temp = Instantiate(temp2, Vector3.zero, Quaternion.Euler(Vector3.zero));
        temp.name = temp2.name;
        if(temp != null){
            if(secondaryWeaponSlot.transform.childCount == 0){
                temp.transform.SetParent(secondaryWeaponSlot.transform);
                temp.SetActive(true);
            } else {
                temp.transform.SetParent(backpackSlot.transform);
                if(backpackSlot.transform.childCount == 1){
                    //First weapon in backpack
                    temp.SetActive(true);
                }
                lengthOfBackpack = backpackSlot.transform.childCount-1;
            }
            temp.GetComponent<RectTransform>().localPosition = Vector3.zero;
            temp.GetComponent<LoadoutWeaponSlot>().weaponToDisplay = gun.gameObject;
        }
    }

    private void ReloadLoadoutText(){
        primaryWeaponText.GetComponent<LoadoutSlotText>().UpdateText();
        secondaryWeaponText.GetComponent<LoadoutSlotText>().UpdateText();
        backpackWeaponText.GetComponent<LoadoutSlotText>().UpdateText();
    }
}
