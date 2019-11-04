using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put on an object that is a trigger
//intent: 

public class BlastMovement : MonoBehaviour
{
    public Transform destination;
    public float damage;

    void Start() {
        Debug.Log("Assigned damage: " + damage);
    }

    void Update()
    {
        transform.LookAt(destination);
        transform.Translate(0, 0, 7f * Time.deltaTime); //swim 5 metres/second
    }

    void OnTriggerEnter(Collider otherObj) {
        Debug.Log("Colliding");
        if (otherObj.gameObject.tag == "enemy") {
            Debug.Log("Colliding with enemy");
            Destroy(gameObject);
            Destroy(otherObj.gameObject);
        }
    }
}