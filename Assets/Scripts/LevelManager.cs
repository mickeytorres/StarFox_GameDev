using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{ // Update is called once per frame
    void Update()
    {
        //eventually will want some kind of "press [ ] to start game"
        if (Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(0);
        } 
    }
}
