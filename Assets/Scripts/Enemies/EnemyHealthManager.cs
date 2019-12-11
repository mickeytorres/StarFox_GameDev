using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    //usage: place on an enemy & turret prefab
    //intent: keeps track of enemy health and damage types

    float enemyHealthScore = 20f;
    float turretHealthScore = 20f; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        
        if(other.CompareTag("SingleLaser")){
            enemyHealthScore -= 4f; //call the numbers from the PlayerShoot script, once we decide if we're doing a state machine or not?
            turretHealthScore -= 4f;  
        } else if(other.CompareTag("DoubleGLaster")){
            enemyHealthScore -= 8f;
            turretHealthScore -= 8f; 
        } else if(other.CompareTag("DoubleBLaser")){
            enemyHealthScore-= 12f;
            turretHealthScore -= 12f; 
        }else if(other.CompareTag("chargedLaser")){
            enemyHealthScore -=16f; 
            turretHealthScore -= 16f;
        }else if(other.CompareTag("Bomb")){
            enemyHealthScore -= 20f;
            turretHealthScore -= 20f; 
        }
        
    }

    void EnemyDeathState(){ //when enemy's health runs out
        if(enemyHealthScore <= 0){
            Destroy(gameObject); 
            //play explosion animation?
        }
    }
}
