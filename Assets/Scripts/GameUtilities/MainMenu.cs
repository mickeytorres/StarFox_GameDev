using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour {
    public Canvas mainMenu;
    public Canvas howToPlay;

    void Awake() {
        gameObject.GetComponent<Menu>().currOption = Menu.Option.FromBeginning;
        Debug.Log("Setting the original option");
    }

    void Update() {
        SelectOption();
    }

    //function to detect when the player presses return/enter and execute the action of the option it's on 
    //(continue or restart)
    public void SelectOption() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (gameObject.GetComponent<Menu>().GetOption() == Menu.Option.FromBeginning) {
                SceneManager.LoadScene(1);
            }

            //in this case, the OtherOption choice will open the "How To Play" menu
            else if (gameObject.GetComponent<Menu>().GetOption() == Menu.Option.OtherOption) {
                howToPlay.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
