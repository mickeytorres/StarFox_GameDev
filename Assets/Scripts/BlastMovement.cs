using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put on an object that is a trigger
//intent: 

public class BlastMovement : MonoBehaviour
{
    public Transform destination;

    void Update()
    {
        transform.LookAt(destination);
        transform.Translate(0, 0, 5f * Time.deltaTime); //swim 5 metres/second
    }

    void OnTriggerEnter(Collider otherObj) {
        if (otherObj.gameObject.tag == "enemy") {
            Debug.Log("Colliding with enemy");
            Destroy(gameObject);
            Destroy(otherObj);
        }
    }
}