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
// - figure out why enemies won't lock onto the player's position like their supposed to? 
// 

public class EnemyBehaviour : MonoBehaviour
{

float enemyHealthScore = 20f; //up this value later, just low now for easy quick testing. 
float rotationAngle;

float thrust;
float thrustR;

float shipPosX;

float shipSpeed;

// bool rightDir; 
public GameObject[] EnemyAI; //not sure if i need this? 
public GameObject Trail;

public GameObject blastPrefab;
//public GameObject um; this is for another bullet prefab, i lost this section of code so idk. i need to look into more. 

public Vector3 finalDestination; 

public Rigidbody rb; 
public Transform targetShip; 



    // Start is called before the first frame update
    void Start()
    {
        thrust = 2.0f;  
        //thrustR = 2.0f; 
        //rightDir = true;
        shipSpeed = 0.5f; 
        shipPosX = EnemyAI[0].transform.position.x;

        rb = GetComponent<Rigidbody>(); 
        
        finalDestination = targetShip.transform.position; 

        
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemyShips();
    
    
    // this is a section from code that Leo wrote, figuring out how to get it into this script or just unpack it to put on the enemyAIs. 
        //  timer += Time.deltaTime;
        // if (timer > 4)
        // {
        //     Instantiate(blastPrefab,transform.position,transform.rotation,MyParent);
        //     timer = 0;
        // }


    }

    void FixedUpdate(){

        if(gameObject.CompareTag("EnemyShip") || gameObject.CompareTag("EnemyTrailShip")){ //i think we can take out this second tag
            rb.AddForce(transform.right * thrust); //makes the ship move forward
        }
        
    }

    void OnTriggerEnter(Collider other){ 
        //if(check if something is in camera view?) {}

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
        transform.LookAt(targetShip); //need the scene and player script to test this works

        if(Vector3.Distance(transform.position, targetShip.position) < 3f){
            flyAway(); 
            //basically if the player gets to the point of passing the enemy, they will EVENTUALLY fly left or right
        }

        if(EnemyAI[0].transform.position.z <= targetShip.transform.position.z){ //instead this should check if the ship is past the player, then makes them "destroy" and player mvoes on
            EnemyAI[0].transform.Translate(Vector3.right * shipSpeed); //i can't check this without having the scene scroller? 
        }

        Bullets();
        
            

    }

    void flyAway(){

        if(shipPosX <= 0f){
            shipPosX = Random.Range(-20, -10);//ship flies away to the left, set the X position in the above Vector to a range to the left
        }else{
            shipPosX = Random.Range(10, 20);//ship flies to the right side of the screen, same range but for the right. 
        }
        
        finalDestination = new Vector3(shipPosX, -1, 5); //this will change eventually, but I need the numbers just sa placeholders

        //butterfly enemies, shooting code, they go up and shoot then go up, and do fly away to a new destination, they'll just call this 
        //event system? butterflies come up based on player's z position (make that a public vector 3 so whoever assembles the )
    }

    void Bullets(){ //do i need this or should i use the enemy bullet scripts? 
        Instantiate(blastPrefab, EnemyAI[0].transform.position, EnemyAI[0].transform.rotation);
        blastPrefab.transform.LookAt(targetShip);
        blastPrefab.transform.Translate(0, 0, 7f * Time.deltaTime); //this needs some tuning for sure, it goes way too fast
        Debug.Log("pew pew"); 

        //PROBLEM/HELP: It's firing under the player, not sure where in the numbers i need to change the destination for the bullets
    }
}
