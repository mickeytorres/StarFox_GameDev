using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: 
//intent: manage scripted movement of team AI

public class TeamAI : MonoBehaviour
{
    public Vector3 myDestination;
    float health = 20f;

    private float lifeSpan = 13.5f;

    void Update() {
        lifeSpan -= Time.deltaTime;
    }

    // void OnTriggerEnter(Collider otherObj) {

    // }

    // //function to handle Team Star Fox members getting hit by Fox :'(
    // void FriendlyFire() {

    // }
}
