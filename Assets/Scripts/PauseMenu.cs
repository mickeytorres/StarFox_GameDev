using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//usage: put this on a canvas with two choices
//intent: 

public class PauseMenu : MonoBehaviour {
    public enum Option {
        Continue,
        Retry,
    }
    public Option currOption = Option.Continue;
    
    public TextMeshProUGUI continueText;
    public TextMeshProUGUI retryText;

    private TextMeshProUGUI currText;
    private TextMeshProUGUI otherText;

    private TextMeshProUGUI[] menuChoices;

    private float switchTime = 1f;

    void Start() {
        menuChoices = new TextMeshProUGUI[] {continueText, retryText};
        currText = continueText;
        otherText = retryText;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) {
            currOption = ChangeOption(currOption);
        }
        BlinkText(currText);
    }

    private Option ChangeOption(Option nextOption) {
        //There's two choices: "Continue" and "Retry Course" --> Make each one its own text or button or whatever, 
        //put them into an array and use the arrow keys to change between the choices?
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

    public Option GetOption() {
        return currOption;
        // if (currOption == Option.Retry && Input.GetKeyDown(KeyCode.Return)) {
        //     return true;
        // } 
        // return false;
    }

    void BlinkText(TextMeshProUGUI currText) {
        float time = Mathf.PingPong(Time.unscaledDeltaTime * 275f, 1f);
        currText.color = Color.Lerp(Color.white, Color.red, time);
        otherText.color = Color.white;
    }
}

