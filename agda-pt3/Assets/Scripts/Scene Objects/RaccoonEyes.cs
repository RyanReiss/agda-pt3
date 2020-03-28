using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonEyes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EndRaccoonEyes(){
        GetComponent<Animator>().SetTrigger("EndRaccoonEyes");
    }
}
