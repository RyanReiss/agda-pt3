using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
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
        barImage.fillAmount = ((float)player.GetComponent<Health>().GetCurrentHealth() / (float)player.GetComponent<Health>().maxHealth);
    }
}
