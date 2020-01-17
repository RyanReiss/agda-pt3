using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class LoadoutBackpackButtons : MonoBehaviour, IPointerClickHandler
{
    public bool rightOrLeft; // true = right, false = left
    public void OnPointerClick(PointerEventData eventData)
    {
        if(rightOrLeft){
            //right button
            transform.parent.GetComponent<LoadoutController>().MoveBackPackRight();
        } else {
            //left button
            transform.parent.GetComponent<LoadoutController>().MoveBackPackLeft();
        }
    }
}
