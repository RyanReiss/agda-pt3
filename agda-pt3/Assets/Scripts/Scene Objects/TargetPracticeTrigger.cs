using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPracticeTrigger : MonoBehaviour
{

    public GameObject cutsceneObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log("Object triggered target cutscene!" + other.gameObject.name);
        if(other.gameObject.GetComponent<Bullet>()){
            EnableCutSceneObject();
        }
    }
    private void EnableCutSceneObject(){
        cutsceneObject.SetActive(true);
    }
}
