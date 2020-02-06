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
    public float msInvulnerability;
    private float invulnEndTime;
    ScreenStaticController screenStaticController;

    public void Start()
    {
        currentHealth = maxHealth;
        screenStaticController = GameObject.FindGameObjectWithTag("ScreenStaticController").GetComponent<ScreenStaticController>();
    }

    public void TakeDamage(float damage){
        if(invulnEndTime <= Time.time){
            invulnEndTime = Time.time + msInvulnerability/1000f;
            currentHealth -= damage;
            if(gameObject.name == "Player"){
                Debug.Log("Starting Static...");
                screenStaticController.StartScreenStatic(currentHealth/maxHealth);
            }
            if(currentHealth <= 0){
                gameObject.SetActive(false);
                if(gameObject.GetComponent<BaseEnemyAI>()){
                    DropTableController.Instance.RollDropTable(gameObject.GetComponent<BaseEnemyAI>().dropTableName,gameObject.transform);
                }
                //Destroy(gameObject);
                if(gameObject.name == "Player"){
                    gameObject.SetActive(true);
                    //GameObject.FindGameObjectWithTag("DebugScreen").gameObject.SetActive(false);
                    SceneManager.LoadScene("GameOverScreen");
                    gameObject.transform.position = Vector3.zero;
                    currentHealth = maxHealth;
                }

            }
        }
    }

    public float GetCurrentHealth(){
        return currentHealth;
    }
}
