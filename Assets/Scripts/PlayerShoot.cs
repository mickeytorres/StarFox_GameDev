using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a user-controlled player character
//intent: press (w) to shoot at enemies

public class PlayerShoot : MonoBehaviour
{
    //prefabs for different types of shots and blasts spawns
    public GameObject singlePrefab;
    public GameObject doubleGPrefab;
    public GameObject doubleBPrefab;
    public GameObject chargedPrefab;
    public GameObject bombPrefab;
    public GameObject blastSpawn;
    public GameObject blastHolder;

    //button variables so player cannot spam and to measure chargin up the laser
    float timeHeld = 0f;
    float startTime = 0f;
    bool canPress = true;
    float coolDown = 0.5f;

    //damage constants
    public float singleDamage = 4f;
    public float doubleGDamage = 8f;
    public float doubleBDamage = 12f;
    public float chargedDamage = 16f;
    public float bombDamage = 20f;

    // Update is called once per frame
    void Update()
    {
        if (!canPress) {
            coolDown -= Time.deltaTime;
        }

        if (coolDown <= 0) {
            coolDown = 0.5f;
            canPress = true;
        }
        
        if(canPress) {
            if (Shoot()) {
                canPress = false;
            }
        }
    }

    bool Shoot() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            startTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            timeHeld = Time.time - startTime;
            Debug.Log(timeHeld);
            if (timeHeld >= 2f) {
                Debug.Log("Releasing charged laser");
                GameObject thisBlast = Instantiate(chargedPrefab, blastSpawn.transform);
                thisBlast.transform.parent = blastHolder.transform;
                return true;
            }
            else {
                Debug.Log("Releasing normal laser");
                GameObject thisBlast = Instantiate(singlePrefab, blastSpawn.transform);
                thisBlast.transform.parent = blastHolder.transform;
                return true;
            }
        }
        return false;
    }
}
