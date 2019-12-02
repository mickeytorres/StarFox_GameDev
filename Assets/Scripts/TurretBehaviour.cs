using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    //usage: place this on a turret prefab and attach it to an enemy prefab
    //intent: shoots bullets at the player, follows where the player is. 

    public GameObject Turret;

    public GameObject blastPrefab; 
    public Transform Player; 

    public float turretHealthScore = 20f;  

    void Start(){
        InvokeRepeating("fireBullets", 1.0f, 0.5f); 
    }

    void Update()
    {
        
    }

    void fireBullets(){
        Instantiate(blastPrefab, Turret.transform.position, Turret.transform.rotation);
        blastPrefab.transform.LookAt(Player);
        blastPrefab.transform.Translate(0, 0, 7f * Time.deltaTime); //bullet speed 
        Debug.Log("pew pew");
    }

    
}
