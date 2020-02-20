using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    Animator animator;
    public Transform transformToReturnTo;
    private void Awake() {
        animator = this.GetComponent<Animator>();
    }
    private void OnEnable() {
        if(animator != null){
            this.GetComponent<Animator>().SetTrigger("StartFlash");
            // Debug.Log("Starting Muzzle Flash");
        }  
    }
    public void EndMuzzleFlash(){
        if(transformToReturnTo != null)
            this.transform.parent = transformToReturnTo;
        this.gameObject.SetActive(false);
        // Destroy(this.gameObject);
    }
}
