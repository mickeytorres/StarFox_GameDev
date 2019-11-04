using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a user-controlled player character
//intent: press (w) to shoot at enemies

public class PlayerShoot : MonoBehaviour
{
    public GameObject blastPrefab; 
    public GameObject blastSpawn;

    bool canPress = true;
    float coolDown = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canPress) {
            Instantiate(blastPrefab, blastSpawn.transform);
            canPress = false;
        }

        if (!canPress) {
            coolDown -= Time.deltaTime;
        }

        if (coolDown <= 0) {
            coolDown = 0.5f;
            canPress = true;
        }
    }
}
