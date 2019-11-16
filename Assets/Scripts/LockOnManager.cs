using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on an empty GameObject with a collider
//intent: detect if something enters this collider and pass that object to a shooting script with a Lock On feature

public class LockOnManager : MonoBehaviour
{
    public GameObject planeBody; 
    public GameObject targetObj;
    
    void OnTriggerEnter(Collider otherObj) {
        if (otherObj.gameObject.tag == "EnemyShip") {
            //pass this object to PlayerShoot.cs
            //PlayerShoot is on Player -> Plane -> PlaneBody
            planeBody.gameObject.GetComponent<PlayerShoot>().chargedTarget = otherObj.gameObject;
        }
    }
    
    public GameObject GetTarget() {
        return null;
    }

}
