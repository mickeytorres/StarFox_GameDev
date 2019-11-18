using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on an empty GameObject with a collider
//intent: detect if something enters this collider and pass that object to a shooting script with a Lock On feature

public class LockOnManager : MonoBehaviour
{
    public GameObject planeBody; 
    
    void OnTriggerEnter(Collider otherObj) {
        //check if an enemy entered this object's trigger zone 
        //if it did, pass it to PlayserShoot so PlayerShoot will know what object to target
        if (otherObj.gameObject.tag == "EnemyShip") {
            planeBody.gameObject.GetComponent<PlayerShoot>().chargedTarget = otherObj.gameObject;
        }
    }
}
