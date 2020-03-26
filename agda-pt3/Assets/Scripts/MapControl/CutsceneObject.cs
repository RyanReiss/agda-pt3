using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneObject : MonoBehaviour
{

    // Script that functions as Cutscene Start
    // Attach to objects that you want to start a cutscene when the player walks into their triggers

    // How to create a cutscene using this class
    // 1. Create an empty GameObject with a collider that is a trigger
    // 2. In the inspector, set the CutsceneSequence corresponding to how you want the cutscene to proceed
    // 3. Make sure each element in the list has the proper parameters filled out before the cutscene is run
    // 4. Now whenever the player walks into that trigger, it will start the cutscene you just created!
    
    [Serializable]
    public class CutSceneAction {
        [Serializable]
        public enum Action
        {
            MovePlayer,
            MoveObject,
            HideOrShowObject,
            ShowDialogueBox,
            PlaySound,
            MoveCamera,
            WaitForTime,
            AnimateObject
        }
        [SerializeField]
        public Action actionToPerform;
        [SerializeField]
        public bool waitUntilActionFinishes;
        [SerializeField]
        public GameObject focusObject;
        [SerializeField]
        public Vector3 positionToMoveTo;
        [SerializeField]
        public List<string> dialogueToSay;
        [SerializeField]
        public string soundOrAnimatonToPlay;
        [SerializeField]
        public float timeToWait;
    }



    public bool disableOnEnd;
    [SerializeField]
    public List<CutSceneAction> cutsceneSequence;
    private int index;
    private bool cutsceneStarted;
    private bool moveToNextAction;

    // Start is called before the first frame update
    void Start()
    {
        index = -1;
        cutsceneStarted = false;
        moveToNextAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(index >= cutsceneSequence.Count){
            Debug.Log("Finished Cutscene");
            index = 0;
            cutsceneStarted = false;
            StopAllCoroutines();
            CameraController.Instance.SetCameraMode(CameraController.Modes.Elastic);
            PlayerController.Instance.gameObject.GetComponent<PlayerController>().SetPlayerLockInPlace(false);
            if(disableOnEnd){
                this.gameObject.SetActive(false);
            }
        }
        if(cutsceneStarted){
            PerformCutscene();
        }
    }

    private void PerformCutscene(){
        if(moveToNextAction){
            Debug.Log("Proceeding to next action...");
            moveToNextAction = false;
            index++;
            if(index < cutsceneSequence.Count){
                if(!cutsceneSequence[index].waitUntilActionFinishes){
                    moveToNextAction = true;
                }
                switch (cutsceneSequence[index].actionToPerform)
                {
                    case CutSceneAction.Action.MovePlayer:
                        StartCoroutine(MovePlayerToLocation(cutsceneSequence[index].positionToMoveTo, index));
                        break;
                    case CutSceneAction.Action.ShowDialogueBox:
                        StartCoroutine(ShowDialogueBox(cutsceneSequence[index].dialogueToSay, index));
                        break;
                    case CutSceneAction.Action.MoveCamera:
                        StartCoroutine(MoveCamera(cutsceneSequence[index].positionToMoveTo, index));
                        break;
                    case CutSceneAction.Action.WaitForTime:
                        StartCoroutine(WaitForTime(cutsceneSequence[index].timeToWait, index));
                        break;
                    case CutSceneAction.Action.MoveObject:
                        StartCoroutine(MoveObject(cutsceneSequence[index].focusObject, cutsceneSequence[index].positionToMoveTo, index));
                        break;
                    case CutSceneAction.Action.HideOrShowObject:
                        StartCoroutine(HideOrShowObject(cutsceneSequence[index].focusObject, index));
                        break;
                    case CutSceneAction.Action.AnimateObject:
                        StartCoroutine(AnimateObject(cutsceneSequence[index].focusObject, cutsceneSequence[index].soundOrAnimatonToPlay, index));
                        break;
                }
            } 
        }
    }

    private IEnumerator MovePlayerToLocation(Vector3 loc, int tempIndex){
        Debug.Log("moving player to: " + loc);
        PlayerController.Instance.SetPlayerLockInPlace(true);
        while(PlayerController.Instance.transform.position != loc){
            PlayerController.Instance.MoveTowardsLocation(loc);
            yield return null;
            Debug.Log("EndOfWhileLoop PlayerPos: " + PlayerController.Instance.transform.position + " : loc = " + loc);
        }
        PlayerController.Instance.SetPlayerLockInPlace(false);
        if(cutsceneSequence[tempIndex].waitUntilActionFinishes){
            moveToNextAction = true;
        }
        Debug.Log("Leaving Enumerator");
    }

    private IEnumerator ShowDialogueBox(List<string> dialogueToShow, int tempIndex){
        PlayerController.Instance.SetPlayerLockInPlace(true);
        StopPlayer();
        yield return new WaitForSeconds(0.2f);
        DialogueController.Instance.InteractWithTextBox(dialogueToShow);
        StopPlayer();
        while(DialogueController.Instance.dialogueBox.activeInHierarchy){
            PlayerController.Instance.SetPlayerLockInPlace(true);
            Debug.Log("Is Player Locked: " + PlayerController.Instance.IsPlayerLockedInPlace());
            if(Input.GetKeyDown(KeyCode.E)){
                DialogueController.Instance.InteractWithTextBox(dialogueToShow);
            }
            yield return null;
        }
        PlayerController.Instance.SetPlayerLockInPlace(false);
        if(cutsceneSequence[tempIndex].waitUntilActionFinishes){
            moveToNextAction = true;
        }
    }

    private IEnumerator MoveCamera(Vector3 positionToMoveTo, int tempIndex){
        CameraController.Instance.SetCameraMode(CameraController.Modes.Fixed);
        PlayerController.Instance.SetPlayerLockInPlace(true);
        yield return null;
        while(CameraController.Instance.transform.position.x != positionToMoveTo.x && CameraController.Instance.transform.position.y != positionToMoveTo.y){
            CameraController.Instance.MoveCameraTowardsPosition(positionToMoveTo);
            yield return null;
        }
        PlayerController.Instance.SetPlayerLockInPlace(false);
        if(cutsceneSequence[tempIndex].waitUntilActionFinishes){
            moveToNextAction = true;
        }
    }

    private IEnumerator WaitForTime(float timeToWait, int tempIndex){
        PlayerController.Instance.SetPlayerLockInPlace(true);
        yield return new WaitForSeconds(timeToWait);
        //PlayerController.Instance.SetPlayerLockInPlace(false);
        if(cutsceneSequence[tempIndex].waitUntilActionFinishes){
            moveToNextAction = true;
        }
    }

    private IEnumerator MoveObject(GameObject focusObject, Vector3 loc, int tempIndex){
        PlayerController.Instance.SetPlayerLockInPlace(true);
        while(focusObject.transform.position != loc){
            focusObject.transform.position = Vector2.MoveTowards(focusObject.transform.position, loc, 10f * Time.deltaTime);
            yield return null;
        }
        PlayerController.Instance.SetPlayerLockInPlace(false);
        if(cutsceneSequence[tempIndex].waitUntilActionFinishes){
            moveToNextAction = true;
        }
    }

    private IEnumerator HideOrShowObject(GameObject focusObject, int tempIndex){
        PlayerController.Instance.SetPlayerLockInPlace(true);
        focusObject.SetActive(!focusObject.activeInHierarchy);
        yield return null;
        PlayerController.Instance.SetPlayerLockInPlace(false);
        if(cutsceneSequence[tempIndex].waitUntilActionFinishes){
            moveToNextAction = true;
        }
    }

    private IEnumerator AnimateObject(GameObject focusObject, string animName, int tempIndex){
        PlayerController.Instance.SetPlayerLockInPlace(true);
        focusObject.GetComponent<Animator>().SetTrigger(animName);
        yield return null;
        while(focusObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animName)){
            yield return null;
        }
        PlayerController.Instance.SetPlayerLockInPlace(false);
        if(cutsceneSequence[tempIndex].waitUntilActionFinishes){
            moveToNextAction = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerController>() && cutsceneStarted == false){
            Debug.Log("Starting Cutscene....");
            StopPlayer();
            index = -1;
            cutsceneStarted = true;
            moveToNextAction = true;
        }
    }

    private void StopPlayer(){
            PlayerController.Instance.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            PlayerController.Instance.velocity = Vector3.zero;
            PlayerController.Instance.gameObject.GetComponent<Animator>().SetBool("PlayerMoving",false);
    }
}
