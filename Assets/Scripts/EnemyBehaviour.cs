using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//USAGE: put this on a EnemyAI PreFab 
//INTENT: enemies start acting when the player enters a certain section of the scene. should contain the screen shake and effects it has on player/companion
//        AI when they collide. As of right now, this script focuses on the "horde" enemies, not the boss. 
//TODO: 
// - screen shake on trigger enter (trigger on the enemy)
//      - on screen shake, screen flahses red/white to indicate health 
// 

public class EnemyBehaviour : MonoBehaviour
{

    float enemyHealthScore = 20f; //up this value later, just low now for easy quick testing. 

    float thrust;

    float shipPosX;

    float shipSpeed;

// bool rightDir; 
    public GameObject[] EnemyAI; //not sure if i need this? 
    public GameObject Trail; //for the trail enemies

    public GameObject blastPrefab;
    //public GameObject um; this is for another bullet prefab, i lost this section of code so idk. i need to look into more. 

    public Vector3 finalDestination; 

    public Rigidbody rb; 
    public Transform targetShip; 



    // Start is called before the first frame update
    void Start()
    {
        thrust = 2.0f; //physics ship movement, stronger than the speed number
        shipSpeed = 0.5f; //ship's speed for transform
        shipPosX = EnemyAI[0].transform.position.x;

        rb = GetComponent<Rigidbody>(); 
        
        finalDestination = targetShip.transform.position; 

        
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemyShips();

    }

    void FixedUpdate(){

        if(gameObject.CompareTag("EnemyShip") || gameObject.CompareTag("EnemyTrailShip")){ //i think we can take out this second tag
            rb.AddForce(transform.right * thrust); //makes the ship move forward
        }
        
    }

    void OnTriggerEnter(Collider other){ 

        if(other.CompareTag("Player")){ 
            enemyHealthScore--; //these numbers will be edited later in the health script
        }else if(other.CompareTag("Bullet")){ //if a bullet goes through this trigger, these tags are changed 
            enemyHealthScore--;
            Debug.Log("Enemy Health: " + enemyHealthScore);
            if(enemyHealthScore <= 0f){
                Destroy(gameObject); //dies when the health runs out
            }
        }
    }


    void moveEnemyShips() //this is for the basic enemy ships
    {
        transform.LookAt(targetShip);

        if(Vector3.Distance(transform.position, targetShip.position) < 3f){
            flyAway(); 
            //player will EVENTUALLY fly left or right after passing enemy
        }

        if(EnemyAI[0].transform.position.z <= targetShip.transform.position.z){ //checks if ship is "behind" the player
            EnemyAI[0].transform.Translate(Vector3.right * shipSpeed); //moves the ship away from player
        }

        enemyShoot();
        
            

    }

    void flyAway(){

        if(shipPosX <= 0f){
            shipPosX = Random.Range(-20, -10);//ship flies away to the left, set the X position in the above Vector to a range to the left
        }else{
            shipPosX = Random.Range(10, 20);//ship flies to the right side of the screen. 
        }
        
        finalDestination = new Vector3(shipPosX, -1, 5);

        //butterfly enemies, shooting code, they go up and shoot then go up, and do fly away to a new destination, they'll just call this 
        //event system? butterflies come up based on player's z position (make that a public vector 3 so whoever assembles the )
    }

    void enemyShoot(){ 
        Instantiate(blastPrefab, EnemyAI[0].transform.position, EnemyAI[0].transform.rotation);
        blastPrefab.transform.LookAt(targetShip);
        blastPrefab.transform.Translate(0, 0, 7f * Time.deltaTime); //bullet speed 
        Debug.Log("pew pew"); 

        //PROBLEM/HELP: It's firing under the player, not sure where in the numbers i need to change the destination for the enemyShoot
    }
}
