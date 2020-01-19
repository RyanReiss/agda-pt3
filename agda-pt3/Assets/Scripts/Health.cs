using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    // A general "health" class that can be put on any entity that needs to have
    // some sort of health system including: enemies, the player, scene objects, etc.

    public float maxHealth;
    private float currentHealth;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            gameObject.SetActive(false);
            //Destroy(gameObject);
            if(gameObject.name == "Player"){
                gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("DebugScreen").gameObject.SetActive(false);
                SceneManager.LoadScene("GameOverScreen");
                gameObject.transform.position = Vector3.zero;
                currentHealth = maxHealth;
            }
        }
    }

    public float GetCurrentHealth(){
        return currentHealth;
    }
}
