using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put on an object that is a trigger
//intent: the blasts shot by player character's laser. Can hit enemy objects in the scene. 

public class BlastMovement : MonoBehaviour
{
    private Vector3 moveTowards;
    public float damage;

    void Start() {
        moveTowards = GameObject.FindWithTag("BlastDestination").transform.position;
    }

    void Update()
    {
        transform.LookAt(moveTowards);
        transform.Translate(0, 0, 100f * Time.deltaTime); 
    }

    void OnTriggerEnter(Collider otherObj) {
        if (otherObj.gameObject.tag == "Back") {
            Destroy(gameObject);
        }

        if (otherObj.gameObject.tag == "EnemyShip") {
            Debug.Log("Colliding with enemy");
            Destroy(gameObject);
        }
    }

    void BombMovement() {
        Collider[] inRange = Physics.OverlapSphere(transform.position, 2f);

        // foreach (Collider enemy in inRange) {
        //     enemy.gameObject.GetComponent<AsteroidManager>().bombRange = true;
        // }
    }
}