using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put on an object that is a trigger
//intent: the blasts shot by player character's laser. Can hit enemy objects in the scene. 

public class BlastMovement : MonoBehaviour
{
    private Vector3 moveTowards;
    public float damage;
    public float moveSpeed;

    public float destroyTimer = 2f;

    void Start() {
        SetType();
        moveTowards = GameObject.FindWithTag("BlastDestination").transform.position;
        Debug.Log(moveSpeed);
    }

    void Update()
    {
        //destroy the blast if it's been more than 2f seconds and hasn't hit anything yet
        destroyTimer -= Time.deltaTime;

        if (destroyTimer <= 0f) {
            Destroy(gameObject);
        }

        transform.LookAt(moveTowards);
        transform.Translate(0, 0, moveSpeed * Time.deltaTime); 
    }

    //Setter for setting type of blast this instance will be.
    //bombs move slower than regular blasts.
    private void SetType() {
        if (gameObject.tag == "Bomb") {
            moveSpeed = 45f;
        }
        else {
            moveSpeed = 100f;
        }
    }

    void OnTriggerEnter(Collider otherObj) {
        if (otherObj.gameObject.tag == "EnemyShip") {
            Debug.Log("Colliding with enemy");
            Destroy(gameObject);
        }
    }

    void BombMovement() {
        Collider[] inRange = Physics.OverlapSphere(transform.position, 2f);

        foreach (Collider enemy in inRange) {
            enemy.gameObject.GetComponent<AsteroidManager>().bombRange = true;
        }
    }
}