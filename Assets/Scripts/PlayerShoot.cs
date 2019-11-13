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
    [HideInInspector]public GameObject laserType;

    public GameObject blastSpawn;
    public GameObject blastHolder;

    //damage constants
    [HideInInspector]public float singleDamage = 4f;
    [HideInInspector]public float doubleGDamage = 8f;
    [HideInInspector]public float doubleBDamage = 12f;
    [HideInInspector]public float chargedDamage = 16f;
    [HideInInspector]public float bombDamage = 20f;

    private int powerupStatus = 0;
    private float damage = 4f;

    [HideInInspector]public int bombCount = 3;
    private int bombMax = 9;

    //button variables so player cannot spam and to measure charging up the laser
    float timeHeld = 0f;
    float startTime = 0f;
    bool canShoot = true;
    float coolDown = 1.75f;

    public int triCounter;
    public float triTimer;

    void Start() {
        laserType = singlePrefab;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();

        //button cool down to not spam BOMB attacks
        if (!canShoot) {
            coolDown -= Time.deltaTime;
        }

        if (coolDown <= 0) {
            coolDown = 1.75f;
            canShoot = true;
        }

        if (bombCount > 0 && canShoot) {
            if (DropBomb()) {
                canShoot = false;
            }
        }
    }

    //detect entering the trigger of a powerup
    void OnTriggerEnter(Collider otherObj) {
        if (otherObj.gameObject.tag == "ShootPowerup" && powerupStatus < 2) {
            powerupStatus += 1;
        }
        
        if (otherObj.gameObject.tag == "BombPowerup") {
            if (bombCount > 6) {
                bombCount = bombMax;
            }

            bombCount += 3;
        }
    }

    //setter to set the type of laser that the player will shoot
    void SetLaser() {
        switch (powerupStatus) {
            case 1:
                damage = doubleGDamage;
                laserType = doubleGPrefab;
                break;
            case 2: 
                damage = doubleBDamage; 
                laserType = doubleBPrefab;
                break;
        }
    }

    //function to shoot
    void Shoot() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            startTime = Time.time;
            triCounter = 3;
            triTimer = 0;
        }
        
        if (Input.GetKey(KeyCode.Space) && triCounter > 0) {
            triTimer -= Time.deltaTime;
            if (triTimer <= 0) {
                triTimer = 0.15f;
                GameObject thisBlast = Instantiate(laserType, blastSpawn.transform);
                thisBlast.transform.parent = blastHolder.transform;
                thisBlast.gameObject.GetComponent<BlastMovement>().damage = damage;
                triCounter--;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            timeHeld = Time.time - startTime;
            if (timeHeld >= 1f) {
                Debug.Log("Releasing charged laser");
                GameObject thisBlast = Instantiate(chargedPrefab, blastSpawn.transform);
                thisBlast.transform.parent = blastHolder.transform;
                thisBlast.gameObject.GetComponent<BlastMovement>().damage = chargedDamage;
            }
        }
    }

    //function to drop a bomb
    bool DropBomb() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            GameObject thisBomb = Instantiate(bombPrefab, blastSpawn.transform);
            thisBomb.transform.parent = blastHolder.transform;
            thisBomb.gameObject.GetComponent<BlastMovement>().damage = damage;
            bombCount--;
            return true;
        }
        
        return false;
    }
}
