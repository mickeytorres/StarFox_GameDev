using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//USAGE: put this on a EnemyAI PreFab, set the Prefab as a trigger 
//INTENT: enemies start acting when the player enters a certain section of the scene. should contain the screen shake and effects it has on player/companion
//        AI when they collide. As of right now, this script focuses on the "horde" enemies, not the boss. 
//TODO:  
// - 
// - screen shake on trigger enter (trigger on the enemy)
//      - player health decreses (by what?)
//      - on screen shake, screen flahses red/white to indicate health -- eventually
// - decreases player health by ## amount when attacked by shots from enemy
// 

public class EnemyBehaviour : MonoBehaviour
{

float enemyHealthScore = 20f; //up this value later, just low now for easy quick testing. 
float rotationAngle;

float thrust;
//float thrustR; this might just get taken out, I don't think it's necessary for our level. 

float shipPosX; 

int shipSpeed;
int bulletDamage;
public int testPlayerHealth; //will be leaving soon, just don't have a way to call into the Player Health.  

bool canShoot = true;

// bool rightDir; 
public GameObject[] EnemyAI; //there's so many enemy types with different behaviours, we need to load them into this list and order them to keep this organized. 
public GameObject Player; //have a distance check instead of actual position, do we want in LevelManager script?

public GameObject rotateAround; 

public GameObject Trail; 

public GameObject blastPrefab; 
public GameObject blastSpawn;

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
        testPlayerHealth = 20; 

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
        _bullets();
        _shootBullets();  
    }

    void FixedUpdate(){

        if(gameObject.CompareTag("EnemyShip") || gameObject.CompareTag("EnemyTrailShip")){
            rb.AddForce(transform.right * thrust); 
            //rb.AddForce(transform.right * thrustR); using this pushes the ship in a weird way ? 
        }
        
    }

    void OnTriggerEnter(Collider other){  

        if(other.CompareTag("Player")){ //do i need to add a tag that says if it's the asteroid and not a ship? is the health damage the same? 
            //screenshake & red flash screen, health decrese for player
            testPlayerHealth -= 1; 
            Debug.Log("Player Health: " + testPlayerHealth); 
        }else if(other.CompareTag("SingleLaser")){ //if a bullet goes through this trigger, all enemy objects are technically triggers? 
            //enemyHealthScore = enemyHealth - ; //this will be determined in the tags?
            enemyHealthScore--; //goes down by 1
            Debug.Log("Enemy Health: " + enemyHealthScore);
            
        }else if(other.CompareTag("DoubleGLaser")){
            enemyHealthScore -= 2; //down by 2
            Debug.Log("Enemy Health: " + enemyHealthScore);
        }else if(other.CompareTag("DoubleBLaser")){
            enemyHealthScore -= 4; //exponentially greater effect?
            Debug.Log("Enemy Health: " + enemyHealthScore);
        }else if(other.CompareTag("ChargedLaser")){
            enemyHealthScore -= 8; //or is this one a one shot kill? 
            Debug.Log("Enemy Health: " + enemyHealthScore);
        }else if(other.CompareTag("Bomb")){
            enemyHealthScore -= 8; //brings enemy score down by 8. 
            Debug.Log("Enemy Health: " + enemyHealthScore);
        }

        if(enemyHealthScore <= 0f){
                Destroy(gameObject); //dies when the health runs out, flourishes can be added later? 
            }
    }


    void spawnEnemyShips() //enemy ships spawn at different spaces here
    {

        //targetShip = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        //EnemyAI[1].transform.LookAt(targetShip); //looks at the player character, "smart" AI, should be connected to the object in the list? 
        transform.LookAt(targetShip); //need the scene and player script to test this works

        if(Vector3.Distance(transform.position, targetShip.position) < 3f){
            flyAway(); 
            //if the player gets to the point of passing the enemy, they will fly left or right based on their on screen position
        }

        if(EnemyAI[1].transform.position.z >= Player.transform.position.z){ //instead this should check if the ship is past the player, then makes them "destroy" and player moves on
            EnemyAI[1].transform.Translate(Vector3.right * shipSpeed); //i can't check this without having the scene scroller? 
        }else if(EnemyAI[1].transform.position.z < Player.transform.position.z){
            Destroy(gameObject); 
        } //should this be a distance check instead? i'm working on it. 
        
            

    }

    void spawnTrailShips() //save this guy for last. 
    {
        //get ship to go in a circle, first things first
        //only runs if this is the type to do a spiral
        EnemyAI[2].transform.RotateAround(Vector3.zero, Vector3.up, 15 * Time.deltaTime);  //rotates around the 0,0,0. x position needs changed. 
        //make these enemies children of empty game objects that they can rotate around

        makeTrail();

        //move in specific patterns, choose randomly between way 1 or 2? 
        //  - i'm working on figuring out the patterns now, didn't get to those this weekend. 
        //  - 1 way: the zig zag type movement
        //  - 2 way: the sprial type movement
        
        //instantiates a trail that CAN'T be run into, it can't just be a trail renderer? 
 

    }

    void flyAway(){ //this happens with ALMOST all enemies but not for the ones that circle the player
    //makes the ships fly away and stop tracking the player
        if(shipPosX < 0f){ //asking: is this ship on the left side of the screen? 
            //ship flies away to the left, set the X position in the above Vector to a range to the left
            shipPosX = Random.Range(-10f, -8f); //these can be tuned better

        }else if (shipPosX > 0f){
            //ship flies to the right side of the screen, same range but for the right.
            shipPosX = Random.Range(8f, 10f);  
        }

        finalDestination = new Vector3(shipPosX, -1, 5); //this will change eventually, but I need the numbers just as placeholders

    }

    void makeTrail(){
        //this has a trail instantiate behind the enemies that need a trail behind them, it's giving me a major headache. 
    }

    void _bullets(){
        blastPrefab.transform.LookAt(targetShip);
        blastPrefab.transform.Translate(0, 0, 7f * Time.deltaTime);
    }

    void _shootBullets(){
        if(canShoot){
            GameObject thisBlast = Instantiate(blastPrefab, blastSpawn.transform);
            thisBlast.GetComponent<BlastMovement>().damage = bulletDamage;
            bulletDamage = 1; 
        }
    }
}
