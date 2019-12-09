using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HowToPlay : MonoBehaviour {
    public TextMeshProUGUI returnToMain;

    public Canvas mainMenu;

    // Update is called once per frame
    void Update() {
        BlinkText();
        if (Input.GetKeyDown(KeyCode.Return)) {
            gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        }
    }

    private void BlinkText() {
        float time = Mathf.PingPong(Time.unscaledDeltaTime * 275f, 1f);
        returnToMain.color = Color.Lerp(Color.white, Color.red, time);
    }
}
