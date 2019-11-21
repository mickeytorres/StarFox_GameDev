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

    public float destroyTimer = 2f;

    void Start() {
        SetSpeed();
        SetDestination();
    }

    void Update()
    {
        //destroy the blast if it's been more than 2f seconds and hasn't hit anything yet
        destroyTimer -= Time.deltaTime;

        if (destroyTimer <= 0f) {
            Destroy(gameObject);
        }

        if (lockedTarget != null) {
            moveTowards = lockedTarget.transform.position;
        }

        //this blast will either just shoot forward if it didn't have a target and will chase a specific
        //enemy if it did
        transform.LookAt(moveTowards);
        transform.Translate(0, 0, moveSpeed * Time.deltaTime); 
    }

    void OnTriggerEnter(Collider otherObj) {
        //check if it entered the trigger zone of an enemy
        if (otherObj.gameObject.tag == "EnemyShip") {
            Debug.Log("Collidng with enemy");
            Destroy(gameObject);
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
        if (gameObject.tag == "Bomb") {
            moveSpeed = 55f;
        }
        else {
            moveSpeed = 100f;
        }
    }
}