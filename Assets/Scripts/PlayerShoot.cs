using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a user-controlled player character
//intent: press (w) to shoot at enemies

public class PlayerShoot : MonoBehaviour
{
    public GameObject blastPrefab; 
    public GameObject blastSpawn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            Instantiate(blastPrefab, blastSpawn.transform);
        }
    }
}
