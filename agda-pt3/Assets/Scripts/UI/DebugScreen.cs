using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{
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
        if(player.gun is ReloadableGun){
            // Adds a current ammo display if there is a gun
            AddDebugLine("Current Weapon Ammo: " + ((ReloadableGun)player.gun).GetCurrentClipSize() +
             " / " + ((ReloadableGun)player.gun).GetCurrentAmmoStored());
        } else {
            AddDebugLine("Current Weapon Ammo: ");
        }
        
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
