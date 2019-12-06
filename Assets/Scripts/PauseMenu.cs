using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//usage: put this on a canvas with two choices
//intent: in-game pause menu (accessed by pressing [ESC]) that allows the player to restart level or continue

public class PauseMenu : MonoBehaviour {

    //enum for the two choices
    public enum Option {
        Continue,
        Retry,
    }
    public Option currOption = Option.Continue;
    
    //text elements
    public TextMeshProUGUI continueText;
    public TextMeshProUGUI retryText;

    private TextMeshProUGUI currText;
    private TextMeshProUGUI otherText;

    void Start() {
        currText = continueText;
        otherText = retryText;
    }

    void Update() {
        //there's only two choices, so pressing either vertical arrow key will get to the other option
        //allow player to move through the menu
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) {
            currOption = ChangeOption(currOption);
        }
        BlinkText(currText);
    }

    //allow the player to change between the two options 
    private Option ChangeOption(Option nextOption) {
        if (nextOption == Option.Continue) {
            nextOption = Option.Retry;
            currText = retryText;
            otherText = continueText;
        }
        else if (nextOption == Option.Retry) {
            nextOption = Option.Continue;
            currText = continueText;
            otherText = retryText;
        }
        
        return nextOption;
    }

    //public Getter to tell the LevelManager (which handles restarting the level) what choices the player selected
    public Option GetOption() {
        return currOption;
    }

    //flash the text to let the player know which choice the pointer is currently on
    //other option is white text
    private void BlinkText(TextMeshProUGUI currText) {
        float time = Mathf.PingPong(Time.unscaledDeltaTime * 275f, 1f);
        currText.color = Color.Lerp(Color.white, Color.red, time);
        otherText.color = Color.white;
    }
}

