using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put on an object that is a trigger
//intent: the blasts shot by player character's laser that can hit enemy objects (ships and asteroids) and deal damage

public class BlastMovement : MonoBehaviour
{
    //variables for managing properties of every individual blast
    private Vector3 moveTowards;
    public GameObject lockedTarget;
    public float damage;
    public float moveSpeed;

    public bool IsAnExplosion = false;
    public bool IsABombExplosion = false;
    public bool IsABomb = false;

    public AudioSource bombSoundSource;

    private float destroyTimer;

    public GameObject Explosion;

    void Start() {
        SetLifeTime();
        SetSpeed();
        SetDestination();
    }

    void Update()
    {
        //destroy the blast if it's been more than destroyTimer's initial value # of seconds and hasn't hit anything yet
        destroyTimer -= Time.deltaTime;

        //decrement the destroyTimer (which is representative of each blast's lifetime)
        if (destroyTimer <= 0f) {
            if (IsABomb) {
                //insantiate a special explosio nfor bomb detonation
                Instantiate(Explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
                Debug.Log("Trying to play sound effect");
                bombSoundSource.Play();
            }
            Destroy(gameObject);
        }

        //move towards a target if one was found 
        if (lockedTarget != null) {
            moveTowards = lockedTarget.transform.position;
        }

        //this blast will either just shoot forward if it didn't have a target and will chase a specific
        //enemy if it did, if it isn't an explosion (like the explosion of bomb for instance.)
        if (!IsAnExplosion) {
            transform.LookAt(moveTowards);
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }
    }

    //helper function for bombs to do radius damage (damage calculations handled by AsteroidManager.cs)
    void BombMovement() {
        Collider[] inRange = Physics.OverlapSphere(transform.position, 2f);

        foreach (Collider enemy in inRange) {
            enemy.gameObject.GetComponent<AsteroidManager>().inbombRange = true;
        }
    }

    //Setter for settig the destination of this blast
    private void SetDestination() {
        if (gameObject.tag == "ChargedLaser" && lockedTarget != null) {
            moveTowards = lockedTarget.transform.position;
        }
        else {
            moveTowards = GameObject.FindWithTag("BlastDestination").transform.position;
        }
    }

    //Setter for setting type of blast this instance will be.
    //bombs move slower than regular blasts.
    private void SetSpeed() {
        if (IsABomb) {
            moveSpeed = 100f;
        }
        else {
            moveSpeed = 200f;
        }
    }

    //Setter for setting the lifetime of types of attacks players can launch: bombs and laser blasts
    private void SetLifeTime() {
        if (IsABomb) {
            if (IsABombExplosion) {
                destroyTimer = 2f;
            }
            else {
                destroyTimer = 1f;
            }
        }

        else {
            destroyTimer = 1f;
        }
    }

    private void OnDestroy() {	
        if(destroyTimer >= 0 || IsABomb)	
            Instantiate(Explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);	
    }
}