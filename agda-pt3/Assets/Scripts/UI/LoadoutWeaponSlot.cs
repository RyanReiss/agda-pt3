using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadoutWeaponSlot : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject weaponToDisplay;

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        transform.localPosition = Vector3.zero;

        // Perform Raycast
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position =  Input.mousePosition;

        List<RaycastResult> raycastResultList =  new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for(int i = 0; i < raycastResultList.Count; i++){
            if(raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>() && raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().enabled && raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>() != this){
                Debug.Log("Swapping: " + raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().weaponToDisplay.name + ", and " + this.weaponToDisplay.name);
                if(raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().transform.parent.name == "BackpackSlot"){
                    if(this.transform.parent.name == "PrimarySlot"){
                        //transform.parent.parent.GetComponent<LoadoutController>().SwapWeaponsInBackPack(weaponToDisplay, true);
                        transform.parent.parent.GetComponent<LoadoutController>().SwapWeapons(weaponToDisplay, raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().weaponToDisplay);
                    } else {
                        //transform.parent.parent.GetComponent<LoadoutController>().SwapWeaponsInBackPack(weaponToDisplay, false);
                        transform.parent.parent.GetComponent<LoadoutController>().SwapWeapons(weaponToDisplay, raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().weaponToDisplay);
                    }
                } else if(this.transform.parent.name == "BackpackSlot"){
                    if(raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().transform.parent.name == "PrimarySlot"){
                        //transform.parent.parent.GetComponent<LoadoutController>().SwapWeaponsInBackPack(raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().weaponToDisplay, true);
                        transform.parent.parent.GetComponent<LoadoutController>().SwapWeapons(weaponToDisplay, raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().weaponToDisplay);
                    } else {
                        //transform.parent.parent.GetComponent<LoadoutController>().SwapWeaponsInBackPack(raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().weaponToDisplay, false);
                        transform.parent.parent.GetComponent<LoadoutController>().SwapWeapons(weaponToDisplay, raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().weaponToDisplay);
                    }
                } else {
                    // Swap primary and secondary
                    transform.parent.parent.GetComponent<LoadoutController>().SwapPrimaryAndSecondary();
                }
                raycastResultList[i].gameObject.GetComponent<LoadoutWeaponSlot>().SwapWithOtherSlot(this);
            }
        }
    }

    public void SwapWithOtherSlot(LoadoutWeaponSlot slot){
        Transform temp = slot.transform.parent;
        slot.transform.SetParent(this.transform.parent);
        this.transform.SetParent(temp);
        slot.transform.localPosition = Vector3.zero;
        this.transform.localPosition = Vector3.zero;
    }
}
