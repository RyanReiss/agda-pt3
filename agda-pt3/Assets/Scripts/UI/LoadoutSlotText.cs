using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutSlotText : MonoBehaviour
{
    Text text;
    public Image slotToDisplay;
    private LoadoutWeaponSlot slot;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        if(slotToDisplay.transform.childCount >= 1){
            slot = slotToDisplay.transform.GetChild(0).GetComponent<LoadoutWeaponSlot>();
        }
    }


    public void UpdateText(){
        if(slotToDisplay.transform.childCount >= 1){
            slot = slotToDisplay.transform.GetChild(0).GetComponent<LoadoutWeaponSlot>();
        }
        if(slot != null){
            if(slot.weaponToDisplay.GetComponent<ReloadableGun>()){
                string temp = "\n-------------\n";
                if(slot.weaponToDisplay.GetComponent<ReloadableGun>().currentEffect != "NoEffect"){
                    temp = "\n"+ slot.weaponToDisplay.GetComponent<ReloadableGun>().currentEffect + " bullets\n";
                }
                text.text = 
                "" + slot.name + temp +
                slot.weaponToDisplay.GetComponent<ReloadableGun>().GetCurrentClipSize() +
                " / " + slot.weaponToDisplay.GetComponent<ReloadableGun>().GetCurrentAmmoStored();
            } else if(slot.weaponToDisplay != null){
                text.text = slot.name;
            } else {
                text.text = "";
            }
        } else {
            if(slotToDisplay.transform.childCount >= 1){
                slot = slotToDisplay.transform.GetChild(0).GetComponent<LoadoutWeaponSlot>();
            }
            if(text != null){
                text.text = "";
            }
        }
        
        }
}
