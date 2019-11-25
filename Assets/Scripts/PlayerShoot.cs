using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a user-controlled player character
//intent: shoot blasts and bombs at enemies
//controls: press space to shoot normal blasts, hold space down to charge Laser, press shift to launch bomb

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
    private GameObject laserPrefab;

    public GameObject blastSpawn;
    public GameObject blastHolder;
    [HideInInspector]public GameObject chargedTarget;

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
    public bool charged = false;
    private bool hasTarget = false;

    public bool readyToShootTarget = false;

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
                    GameObject thisBlast = Instantiate(laserPrefab, blastSpawn.transform);
                    thisBlast.transform.parent = blastHolder.transform;
                    thisBlast.gameObject.GetComponent<BlastMovement>().damage = damage;
                    triCounter--;
                }
            }

            //check to see how long [SPACE] was held and if it was long enough to charge up the Laser
            timeHeld = Time.time - startTime;

            if (timeHeld >= 1f && !hasTarget) {
                Debug.Log("Held for : " + timeHeld);
                charged = true;
                timeHeld = 0;
            }
        }

        //releasing a charged Laser
        if (charged && !readyToShootTarget) {
            //turn on the collider to check if something enters the lock-on zone for a charged Laser
            //targetCheck.GetComponent<MeshCollider>().enabled = true;

            chargedTarget = GetTarget();

            //if target not found and [SPACE] released, launch charged Laser
            if (Input.GetKeyUp(KeyCode.Space) && chargedTarget == null && !hasTarget) {
                FireCharged();
            }            
            //if a target was found, then [SPACE] must be released before being able to be pressed again to
            //release a charged Laser that targets a specific enemy 
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

    //function to find the target for a charged laser to follow
    private GameObject GetTarget() {
        Ray enemyDetectRay = new Ray(transform.position, Vector3.forward);
        RaycastHit rayHit = new RaycastHit();
        float enemyDetectRayDist = 15f;
        bool itemFound = Physics.SphereCast(enemyDetectRay, 1f, out rayHit, enemyDetectRayDist);

        Debug.Log("Drawing raycast");
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
        charged = false;
        hasTarget = false;
        //targetCheck.GetComponent<MeshCollider>().enabled = false;
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