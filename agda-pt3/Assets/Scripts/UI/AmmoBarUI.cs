using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarUI : MonoBehaviour
{
    private Image barImage;
    private PlayerController player;

    private void Start()
    {
        barImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(player.GetCurrentWeapon().GetComponent<ReloadableGun>()){
            barImage.fillAmount = ((float)player.GetCurrentWeapon().GetComponent<ReloadableGun>().GetCurrentClipSize() / (float)player.GetCurrentWeapon().GetComponent<ReloadableGun>().maxClipSize);
        } else {
            barImage.fillAmount = 0;
        }
    }
}
