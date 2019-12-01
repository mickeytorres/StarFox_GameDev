﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a user-controlled player character
//intent: shoot blasts and bombs at enemies
//controls: press space to shoot normal blasts, hold space down to charge Laser, press shift to launch bomb

#pragma warning disable XY

public class PlayerShoot : MonoBehaviour { 

    //damage constants
    enum Laser {
        Single = 4,
        DoubleG = 8,
        DoubleB = 12,
    }
    const float chargedDamage = 16f;
    const float bombDamage = 20f;

    Laser currLaserType = Laser.Single;

    //prefabs for different types of shots and blasts spawns
    public GameObject singlePrefab;
    public GameObject doubleGPrefab;
    public GameObject doubleBPrefab;
    public GameObject chargedPrefab;
    public GameObject bombPrefab; 
    public GameObject targetCheck;
    //setter variable that will change the prefab associated with each laser type
    private GameObject laserPrefab;

    public GameObject blastSpawn;
    public GameObject blastHolder;

    //variables for handling triple shot--holding down space (indefinitely) will only release
    //up to 3 blasts. Will not fire indefinitely
    private int triCounter;
    private float triTimer;

    //variables to handle powering up Lasers and picking up more bombs
    private int damage = 4;
    private int bombCount = 3;
    private int bombMax = 9;

    //button variables so player cannot spam bomb and to measure charging up (and locking on) Laser
    private float timeHeld = 0f;
    private float startTime = 0f;
    private bool canShoot = true;
    private float coolDown = 1.75f;

    //variables to handle the charged laser looking for/locking onto a target
    private GameObject chargedTarget;
    private bool charged = false;
    private bool hasTarget = false;
    private bool readyToShootCharged = false;

    void Start() { 
        chargedTarget = null;
        laserPrefab = singlePrefab;
    }

    // Update is called once per frame
    void Update()
    {
        //call shoot. Checks for actual shooting are handled within the function
        TryShooting();

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
    
    //detect entering the trigger of a powerup (for powering up Lasers and replenishing bombs)
    void OnTriggerEnter(Collider otherObj) {
        if (otherObj.gameObject.tag == "ShootPowerup") {
            currLaserType = UpgradeLaser(currLaserType);
        }
        
        if (otherObj.gameObject.tag == "BombPowerup") {
            if (bombCount > 6) {
                bombCount = bombMax;
            }

            bombCount += 3;
        }
    }

    //function to shoot
    private void TryShooting() {
        if (Input.GetKeyDown(KeyCode.Space) && !charged && !readyToShootCharged) {
            startTime = Time.time;
            triCounter = 3;
            triTimer = 0;
            readyToShootCharged = false;
            timeHeld = 0f;
        }
        
        //quickly tapping the spacebar will only release one blast, but holding it down for some amount of time
        //will release a triple shot--holding indefinitely does not release blasts indefinitely
        if (Input.GetKey(KeyCode.Space) && !charged && !readyToShootCharged) {
            if (triCounter > 0) {
                triTimer -= Time.deltaTime;
                if (triTimer <= 0) {
                    triTimer = 0.15f;
                    GameObject thisBlast = Instantiate(laserPrefab, blastSpawn.transform);
                    thisBlast.transform.parent = blastHolder.transform;
                    thisBlast.gameObject.GetComponent<BlastMovement>().damage = damage;
                    triCounter--;
                }
            }

            //check to see how long [SPACE] was held and if it was long enough to charge up the Laser
            timeHeld = Time.time - startTime;

            if (timeHeld >= 1f) {
                Debug.Log("Held for: " + timeHeld);
                charged = true;
                timeHeld = 0f;
            }
        }

        //releasing a charged Laser
        if (charged) {
            chargedTarget = GetTarget();

            if (Input.GetKeyUp(KeyCode.Space)) {
                readyToShootCharged = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && readyToShootCharged && charged) {
            FireCharged();
        }
    }

    //function to find the target for a charged laser to follow
    private GameObject GetTarget() {
        Ray enemyDetectRay = new Ray(transform.position, Vector3.forward);
        RaycastHit rayHit = new RaycastHit();
        float enemyDetectRayDist = 50f;
        bool itemFound = Physics.SphereCast(enemyDetectRay, 2f, out rayHit, enemyDetectRayDist);

        Debug.DrawRay(enemyDetectRay.origin, enemyDetectRay.direction * enemyDetectRayDist, Color.blue);

        if (itemFound) 
            return rayHit.collider.gameObject;
        else 
            return null;
    }

    //function to fire a charged Laser.
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
        chargedTarget = null;
        charged = false;
        readyToShootCharged = false;
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

    //function to upgrade the laser
    private Laser UpgradeLaser(Laser newLaser) {
        if (newLaser == Laser.Single) {
            newLaser = Laser.DoubleG;
            laserPrefab = doubleGPrefab;
        }
        else if (newLaser == Laser.DoubleG) {
            newLaser = Laser.DoubleB;
            laserPrefab = doubleBPrefab;
        }

        damage = (int)newLaser;

        return newLaser;
    }
}