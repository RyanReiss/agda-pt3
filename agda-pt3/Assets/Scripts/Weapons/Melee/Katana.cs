using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Melee
{
    // Start is called before the first frame update
    private float timeCount = 0f;
    // The players
    private KatanaBlade blade;
    private float msSwingTime = 2000f;
    private float timeUnitlDisabled;
    
    void Start()
    {
        fireRate = 0.5f;
        swingSpeed = 20.0f;
        dmg = 10.0f;
        KatanaBlade aBlade = Instantiate(meleePrefab.GetComponent<KatanaBlade>(), spawnPos.position, spawnPos.rotation);
        blade = aBlade;
        blade.transform.position = spawnPos.position;
        blade.transform.SetParent(transform);
        blade.gameObject.SetActive(false);
        timeUnitlDisabled = 0;
    }


    public override void UpdateWeapon(){
        timeCount += Time.deltaTime;
        // Disable the blade if its enabled in the heirarchy and the timeUntilDisabled is less than the current time 
        if(blade.gameObject.activeSelf){
            blade.transform.position = spawnPos.position;
            if(timeUnitlDisabled <= Time.time){
                blade.gameObject.SetActive(false);
            }
        }
        // aBlade.transform.RotateAround(spawnPos.position, new Vector3(0, 0, 1), Time.deltaTime * 20);
        Attack();
    }

    // Fires the player's gun if Left Mouse-Button is pressed
    public override void Attack()
    {
        if (timeCount >= fireRate && Input.GetMouseButton(0))
        {
            blade.ClearListOfCollisions();
            timeUnitlDisabled = Time.time + msSwingTime/1000f;
            blade.transform.position = spawnPos.position;
            blade.gameObject.SetActive(true);
            timeCount = 0f;
        }
    }

    /*public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Health>() != null)
        {
            col.gameObject.GetComponent<Health>().TakeDamage(dmg);
        }
    }*/

}
