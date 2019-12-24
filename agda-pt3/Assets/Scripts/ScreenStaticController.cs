using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenStaticController : MonoBehaviour
{
    
    public Sprite[] statics = new Sprite[4];
    int i = 0;
    int j = 0;
    public int test;
    Image component;

    // Start is called before the first frame update
    void Start()
    {
        component = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(j % test == 0){
            Debug.Log(i);
            component.sprite = statics[i];
            i++;
            if(i >= statics.Length){
                i = 0;
            }
        }
        j++;
    }
}
