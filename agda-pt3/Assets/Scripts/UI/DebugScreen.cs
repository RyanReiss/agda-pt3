using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{

    // A script for creating a screen that overlays the game that gives useful
    // information on stuff occurring in the game. For example, somethings to put
    // on the debug screen are Player health, player energy, player ammo, enemies left, etc.

    Text debugText;
    string sText;
    ArrayList sLines = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        debugText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDebugText();
        debugText.text = sText;
    }

    private void UpdateDebugText(){
        PlayerController player = ((PlayerController)FindObjectOfType(typeof(PlayerController))); // Player Object
        if(player.GetCurrentWeapon().GetComponent<Weapon>() is ReloadableGun){
            // Adds a current ammo display if there is a gun
            AddDebugLine("Current Weapon Ammo: " + ((ReloadableGun)player.GetCurrentWeapon().GetComponent<Weapon>()).GetCurrentClipSize() +
             " / " + ((ReloadableGun)player.GetCurrentWeapon().GetComponent<Weapon>()).GetCurrentAmmoStored());
        } else {
            AddDebugLine("Current Weapon Ammo: ");
        }

        AddDebugLine("Current Health: " + player.GetComponent<Health>().GetCurrentHealth() + " / " + player.GetComponent<Health>().maxHealth); // The player current health

        AddDebugLine("Current Energy: " + player.GetEnergy()); // the player's current sprinting energy
        
        ArrayList temp = (ArrayList)sLines.Clone();
        sText = "";
        foreach (object s in sLines) {
            sText += (string)s + "\n";
            temp.Remove(s);
        }
        sLines = temp;
    }

    private void AddDebugLine(string newLine){
        sLines.Add(newLine + "\n");
    }

}
