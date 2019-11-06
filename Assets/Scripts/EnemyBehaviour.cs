using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//USAGE: put this on a EnemyAI PreFab, collider should be a trigger for asteroids, collider on enemy[Trail]Ships 
//INTENT: enemies start acting when the player enters a certain section of the scene. should contain the screen shake and effects it has on player/companion
//        AI when they collide. As of right now, this script focuses on the "horde" enemies, not the boss. 
//TODO:  
// - spawn enemy script (for calling when triggered by player?)
//      - spawn in random areas, move in a "random" (restricted-ish) way
//      - three types of spawns, asteroids & enemyShips & enemyShipsTrail
//              - 
// - screen shake on trigger enter (trigger on the enemy)
//      - player health decreses
//      - on screen shake, screen flahses red/white to indicate health
// - decreases player health by ## amount when attacked by shots from enemy[Trail]Ships
// 

public class EnemyBehaviour : MonoBehaviour
{

float enemyHealthScore = 20f; //up this value later, just low now for easy quick testing. 
float rotationAngle;

float thrust;
float thrustR;

float shipPosX;

int shipSpeed;

// bool rightDir; 
public GameObject[] EnemyAI; //not sure if i need this? 
public GameObject Player; //have a distance check instead of actual position, do we want in scene script 

public GameObject Trail; 

public Vector3 finalDestination; 

public Rigidbody rb; 

[HideInInspector]
public Transform targetShip; 



    // Start is called before the first frame update
    void Start()
    {
        thrust = 2.0f;  
        //thrustR = 2.0f; 
        //rightDir = true;
        shipSpeed = 1; 
        shipPosX = EnemyAI[1].transform.position.x;

        rb = GetComponent<Rigidbody>(); 
        
        if( targetShip != null ){
            Transform targetShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //make sure to put "Player" tag on player
        }
        finalDestination = targetShip.transform.position; 

        
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemyShips();
    }

    void FixedUpdate(){

        if(gameObject.CompareTag("EnemyShip") || gameObject.CompareTag("EnemyTrailShip")){
            rb.AddForce(transform.right * thrust); 
            //rb.AddForce(transform.right * thrustR); using this pushes the ship in a weird way ? 
        }
        
    }

    void OnTriggerEnter(Collider other){ //will this be a trigger or collider 
        //if(check if something is in camera view?) {}
         
        // spawnEnemyShips();
        // spawnTrailShips(); 

        if(other.CompareTag("Player")){ //do i need to add a tag that says if it's the asteroid and not a ship? 
            //screenshake & red flash screen, health decrese for player
        }else if(other.CompareTag("Bullet")){ //if a bullet goes through this trigger, all enemy objects are technically triggers? 
            enemyHealthScore--;
            Debug.Log("Enemy Health: " + enemyHealthScore);
            if(enemyHealthScore <= 0f){
                Destroy(gameObject); //dies when the health runs out, flourishes can be added later? 
            }
        }
    }


    void spawnEnemyShips() //enemy ships spawn at different spaces here
    {

        //targetShip = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        //EnemyAI[1].transform.LookAt(targetShip); //looks at the player character, "smart" AI, should be connected to the object in the list? 
        transform.LookAt(targetShip); //need the scene and player script to test this works

        if(Vector3.Distance(transform.position, targetShip.position) < 3f){
            flyAway(); 
            //basically if the player gets to the point of passing the enemy, they will EVENTUALLY fly left or right, 
            //based on their on screen position
        }

        if(EnemyAI[1].transform.position.z <= Player.transform.position.z){ //instead this should check if the ship is past the player, then makes them "destroy" and player mvoes on
            EnemyAI[1].transform.Translate(Vector3.right * shipSpeed); //i can't check this without having the scene scroller? 
        }
        
            

    }

    void spawnTrailShips() //asteroids spawn at different spaces here, save this guy for last. 
    {
        //get ship to go in a circle, first things first
        

        //move in specific patterns, choose randomly between way 1 or 2? 
        //  - i'm working on figuring out the patterns now, didn't get to those this weekend. 
        //  - 1 way: the zig sag type movement
        //  - 2 way: the sprial type movement
        
        //instantiates a trail that CAN'T be run into - make trail object with a collider on it, player effect, sotre that list here: 
 

    }

    void flyAway(){
        finalDestination = new Vector3(0, -1, 5); //this will change eventually, but I need the numbers just sa placeholders

        if(shipPosX <= 0f){
            //ship flies away to the left, set the X position in the above Vector to a range to the left
        }else{
            //ship flies to the right side of the screen, same range but for the right. 
        }
    }
}
