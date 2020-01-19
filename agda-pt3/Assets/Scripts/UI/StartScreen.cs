using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Reached");
        button = transform.GetChild(0).gameObject;
        button.SetActive(false);
        StartCoroutine(WaitToDisplay(5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        SceneManager.LoadScene("TilemapTestScene");
    }

    protected IEnumerator WaitToDisplay(float msWaitTime){
        yield return new WaitForSeconds(msWaitTime);
        button.gameObject.SetActive(true);
    }
}
