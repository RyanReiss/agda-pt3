using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    Animator animator;
    public Transform transformToReturnTo;
    private void Awake() {
        animator = this.GetComponent<Animator>();
    }
    private void OnEnable() {
        if(animator != null){
            this.GetComponent<Animator>().SetTrigger("Restart");
        }  
    }
    public void EndBulletImpact(){
        if(transformToReturnTo != null)
            this.transform.parent = transformToReturnTo;
        this.gameObject.SetActive(false);
        // Destroy(this.gameObject);
    }
}
