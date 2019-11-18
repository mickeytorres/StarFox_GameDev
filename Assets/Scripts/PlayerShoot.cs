using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a user-controlled player character
//intent: shoot blasts and bombs at enemies
//controls: press space to shoot normal blasts, hold space down to charge laser, press shift to launch bomb

public class PlayerShoot : MonoBehaviour
{
    //prefabs for different types of shots and blasts spawns
    public GameObject singlePrefab;
    public GameObject doubleGPrefab;
    public GameObject doubleBPrefab;
    public GameObject chargedPrefab;
    public GameObject bombPrefab; 
    public GameObject targetCheck;
    [HideInInspector]public GameObject laserType;

    public GameObject blastSpawn;
    public GameObject blastHolder;
    [HideInInspector]public GameObject chargedTarget;

    //damage constants for different types of lasers and the bomb
    [HideInInspector]public float singleDamage = 4f;
    [HideInInspector]public float doubleGDamage = 8f;
    [HideInInspector]public float doubleBDamage = 12f;
    [HideInInspector]public float chargedDamage = 16f;
    [HideInInspector]public float bombDamage = 20f;

    //variables for handling triple shot--holding down space (indefinitely) will only release
    //up to 3 blasts. Will not fire indefinitely
    private int triCounter;
    private float triTimer;

    //variables to handle powering up lasers and picking up more bombs
    private int powerupStatus = 1;
    private float damage = 4f;
    [HideInInspector]public int bombCount = 3;
    private int bombMax = 9;

    //button variables so player cannot spam bomb and to measure charging up (and locking on) laser
    float timeHeld = 0f;
    float startTime = 0f;
    bool canShoot = true;
    float coolDown = 1.75f;
    public bool charged = false;
    private bool hasTarget = false;

    public bool readyToShootTarget = false;

    void Start() {
        laserType = singlePrefab;
        chargedTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        //call shoot. Checks for actual shooting are handled within the function
        Shoot();

        //button cool down to not spam bombs 
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
    
    //detect entering the trigger of a powerup (for powering up lasers and replenishing bombs)
    void OnTriggerEnter(Collider otherObj) {
        if (otherObj.gameObject.tag == "ShootPowerup" && powerupStatus < 2) {
            powerupStatus += 1;
            SetLaser();
        }
        
        if (otherObj.gameObject.tag == "BombPowerup") {
            if (bombCount > 6) {
                bombCount = bombMax;
            }

            bombCount += 3;
        }
    }

    //function to shoot
    private void Shoot() {
        Debug.Log("Charged status: " + charged);

        if (Input.GetKeyDown(KeyCode.Space) && !charged && !hasTarget) {
            startTime = Time.time;
            triCounter = 3;
            triTimer = 0;
            readyToShootTarget = false;
        }
        
        //quickly tapping the spacebar will only release one blast, but holding it down for some amount of time
        //will release a triple shot--holding indefinitely does not release blasts indefinitely
        if (Input.GetKey(KeyCode.Space) && !charged) {
            if (triCounter > 0) {
                triTimer -= Time.deltaTime;
                if (triTimer <= 0) {
                    triTimer = 0.15f;
                    GameObject thisBlast = Instantiate(laserType, blastSpawn.transform);
                    thisBlast.transform.parent = blastHolder.transform;
                    thisBlast.gameObject.GetComponent<BlastMovement>().damage = damage;
                    triCounter--;
                }
            }

            //check to see how long [SPACE] was held and if it was long enough to charge up the laser
            timeHeld = Time.time - startTime;

            if (timeHeld >= 1f && !hasTarget) {
                Debug.Log("Held for : " + timeHeld);
                charged = true;
                timeHeld = 0;
            }
        }

        //releasing a charged laser
        if (charged && !readyToShootTarget) {
            //turn on the collider to check if something enters the lock-on zone for a charged laser
            targetCheck.GetComponent<MeshCollider>().enabled = true;

            //if target not found and [SPACE] released, launch charged laser
            if (Input.GetKeyUp(KeyCode.Space) && chargedTarget == null && !hasTarget) {
                FireCharged();
            }            
            //if a target was found, then [SPACE] must be released before being able to be pressed again to
            //release a charged laser that targets a specific enemy 
            else if (Input.GetKeyUp(KeyCode.Space) && chargedTarget != null) {
                hasTarget = true;
                charged = false;
                readyToShootTarget = true;
            }
        }

        if (hasTarget && Input.GetKeyDown(KeyCode.Space) && readyToShootTarget) {
            hasTarget = false;
            FireCharged();
        }
    }

    //function to fire a charged laser.
    private void FireCharged() {
        //instantiate the blast 
        GameObject thisBlast = Instantiate(chargedPrefab, blastSpawn.transform);
        thisBlast.transform.parent = blastHolder.transform;
        thisBlast.gameObject.GetComponent<BlastMovement>().damage = damage;

        //if the charged blast found a target to follow, set the target.
        //this also handles if the target was destroyed before you released the blast
        if (chargedTarget != null) {
            thisBlast.gameObject.GetComponent<BlastMovement>().lockedTarget = chargedTarget;
        } 

        FireChargedHelper();
    }

    //helper function that simply resets all variables managing a charged blast
    private void FireChargedHelper() {
        charged = false;
        hasTarget = false;
        targetCheck.GetComponent<MeshCollider>().enabled = false;
        chargedTarget = null;
    }

    //function to drop a bomb
    private bool DropBomb() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            GameObject thisBomb = Instantiate(bombPrefab, blastSpawn.transform);
            thisBomb.transform.parent = blastHolder.transform;
            thisBomb.gameObject.GetComponent<BlastMovement>().damage = damage;
            bombCount--;
            return true;
        }
        
        return false;
    }

    //setter function to set the type of laser that the player will shoot (charged lasers not included)
    private void SetLaser() {
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
}