using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenStaticController : MonoBehaviour
{
    
    // A WIP progress script for creating screen effects. Specifically, the script is for
    // creating a static effect on the screen when the player gets hit

    public List<Sprite> staticMaterials = new List<Sprite>();
    Image templateImage;
    public float msStaticTime;
    private float currentStaticTime;
    private List<Image> staticImages = new List<Image>();
    public GameObject staticObjectPrefab;
    private float coefficient;

    // Start is called before the first frame update
    void Start()
    {
        templateImage = GetComponent<Image>();
        foreach (Sprite s in staticMaterials)
        {
            GameObject g = Instantiate(staticObjectPrefab,this.transform);
            g.GetComponent<Image>().sprite = s;
            staticImages.Add(g.GetComponent<Image>());
            g.SetActive(false);
        }
    }

    void Update() {
        if(currentStaticTime >= Time.time){
            HideCurrentStatic();
            ShowStaticFrame();
        } else {
            HideCurrentStatic();
        }
    }

    // coef is a value between [0-1]. 0 will give the most static, 1 will give the least static possible.
    public void StartScreenStatic(float coef){
        int staticIndex = Mathf.RoundToInt(coef * staticMaterials.Count);
        coefficient = coef;
        currentStaticTime = Time.time + msStaticTime/1000f;
    }

    private void ShowStaticFrame(){
        // Calculate the next static effect(s) to use
        // Coef values:
        //  [0.00-0.33] = 6 statics
        //  (0.33-0.66] = 3 statics
        //  (0.66-1.00] = 1 static
        if(coefficient >= 0f && coefficient < 0.33f){
            ShowStaticFrames(6);
        } else if(coefficient >= 0.33f && coefficient < 0.66f){
            ShowStaticFrames(3);
        } else if(coefficient >= 0.66f && coefficient <= 1.00f){
            ShowStaticFrames(1);
        }
    }

    private void ShowStaticFrames(int staticsToShow){
        for(int i = 0; i < staticsToShow; i++){
            int randomStatic = Random.Range(0,staticImages.Count);
            //Debug.Log("Random: " + randomStatic);
            while(staticImages[randomStatic].gameObject.activeInHierarchy){
                // Ensure that the frame being chosen to show hasnt been chosen already
                //Debug.Log("Random: " + randomStatic);
                randomStatic = Random.Range(0,staticImages.Count);
            }
            staticImages[randomStatic].gameObject.SetActive(true);
        }
    }

    private void HideCurrentStatic(){
        foreach (Image i in staticImages){
            i.gameObject.SetActive(false);
        }
    }
}
