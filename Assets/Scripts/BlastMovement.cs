using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put on an object that is a trigger
//intent: the blasts shot by player character's laser. Can hit enemy objects in the scene. 

public class BlastMovement : MonoBehaviour
{
    public Transform destination;
    public float damage;

    void Start() {
        Debug.Log("Assigned damage: " + damage);
        destination = GameObject.FindWithTag("BlastDestination").transform;
    }

    void Update()
    {
        transform.LookAt(destination);
        transform.Translate(0, 0, 7f * Time.deltaTime); 
    }

    void OnTriggerEnter(Collider otherObj) {
        Debug.Log("Colliding");
        if (otherObj.gameObject.tag == "EnemyShip") {
            Debug.Log("Colliding with enemy");
            Destroy(gameObject);
        }
    }
}