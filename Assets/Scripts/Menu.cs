using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//usage: put this on a canvas with two choices, one of which is to start a level from the beginning
//intent: 

public class Menu : MonoBehaviour {
    //enum for the two choices
    public enum Option {
        FromBeginning,
        OtherOption,
    }
    [HideInInspector]public Option currOption;
    
    //text elements
    public TextMeshProUGUI otherOptionText;
    public TextMeshProUGUI fromBeginningText;

    private TextMeshProUGUI currText;
    private TextMeshProUGUI otherText;

    void Start() {
        Debug.Log("Option is: " + currOption.ToString());
        if (currOption == Option.OtherOption) {
            currText = otherOptionText;
            otherText = fromBeginningText;
        }
        else if (currOption == Option.FromBeginning) {
            currText = fromBeginningText;
            otherText = otherOptionText;
        }
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
        if (nextOption == Option.OtherOption) {
            nextOption = Option.FromBeginning;
            currText = fromBeginningText;
            otherText = otherOptionText;
        }
        else if (nextOption == Option.FromBeginning) {
            nextOption = Option.OtherOption;
            currText = otherOptionText;
            otherText = fromBeginningText;
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

