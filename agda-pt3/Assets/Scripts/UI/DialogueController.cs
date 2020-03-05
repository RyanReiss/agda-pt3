using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    public GameObject dialogueBox;
    public Text dialogueText;
    List<string> queuedText;
    int currentQueuedTextIndex;
    int currentTextToWriteIndex;
    PlayerController player;

    private static DialogueController _instance;
    public static DialogueController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = this.transform.Find("Dialogue Box").gameObject;
        dialogueText = dialogueBox.transform.Find("Dialogue Text").GetComponent<Text>();
        dialogueBox.SetActive(false);
        queuedText = new List<string>();
        dialogueText.text = "";
        InvokeRepeating("UpdateText",0.1f,0.050f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update() {
        if(dialogueBox.activeInHierarchy){
            if(Input.anyKey && (player.velocity.magnitude >= 10f)){
                CloseDialogBox();
            }
        }
    }

    private void UpdateText(){
        if(dialogueBox.activeInHierarchy && queuedText.Count > 0 && currentQueuedTextIndex < queuedText.Count){ // If the dialog box is open and not outside array bounds
            if(currentTextToWriteIndex < queuedText[currentQueuedTextIndex].Length){ // If the currentText still has more text to typewrite
                // Typewrite the next letter
                // If the current letter
                if(queuedText[currentQueuedTextIndex][currentTextToWriteIndex] == '~'){
                    dialogueText.text = dialogueText.text;
                } else {
                    dialogueText.text = dialogueText.text + queuedText[currentQueuedTextIndex][currentTextToWriteIndex];
                }
                currentTextToWriteIndex++;
            }
        } else if(currentQueuedTextIndex > queuedText.Count){ // Otherwise, close the dialog box if youre outside array bounds as to not cause errors.
            CloseDialogBox();
        }
    }

    public void InteractWithTextBox(List<string> newText){
        if(newText == queuedText){
            InteractWithTextBox();
        } else {
            queuedText = newText;
            dialogueText.text = "";
            currentTextToWriteIndex = 0;
            currentQueuedTextIndex = 0;
            InteractWithTextBox();
        }
    }

    public void InteractWithTextBox(){
        //Debug.Log("Interacting... currentQueuedTextIndex: " + currentQueuedTextIndex + "  Size of queuedText: " + queuedText.Count);
        if(dialogueBox.activeSelf && queuedText.Count > currentQueuedTextIndex){ // If the dialogBox still has text left to show
            if(queuedText[currentQueuedTextIndex].Length > currentTextToWriteIndex){ // If the currentText.Length > the current index...
                currentTextToWriteIndex = queuedText[currentQueuedTextIndex].Length; // Set the text box to the entire currentText
                dialogueText.text = queuedText[currentQueuedTextIndex].Replace("~","");
            } else if(currentQueuedTextIndex < queuedText.Count){ // Otherwise, go to the next set of text (if all the text in the current queuedText has been shown)
                // *** Shouldnt reach here if there is no more text to show
                NextSetOfText();
            } else {
                CloseDialogBox();
            }
        } else {
            OpenDialogBox();
        }
    }

    public void ToggleDialogueBox(){
        if(dialogueBox.activeSelf){
            CloseDialogBox();
        } else {
            OpenDialogBox();
        }
    }

    public void OpenDialogBox(){
        dialogueBox.SetActive(true);
    }

    // When the dialogBox is opened with this method, it will display the text box with the first element of the list,
    // Then any furthur interactions with the text box will make the text box go to the next element in the List
    public void OpenDialogBox(List<string> textToShow){
        dialogueText.text = "";
        dialogueBox.SetActive(true);
        queuedText = textToShow;
    }

    public void CloseDialogBox(){
        dialogueText.text = "";
        currentQueuedTextIndex = 0;
        currentTextToWriteIndex = 0;
        dialogueBox.SetActive(false);
    }

    private void NextSetOfText(){
        dialogueText.text = "";
        currentQueuedTextIndex++;
        if(currentQueuedTextIndex >= queuedText.Count){
            CloseDialogBox();
        } else {
            currentTextToWriteIndex = 0;
        }
    }
}
