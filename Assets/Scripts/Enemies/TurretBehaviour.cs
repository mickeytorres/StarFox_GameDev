using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    //usage: place this on a turret prefab and attach it to an enemy prefab
    //intent: shoots bullets at the player, follows where the player is. 

    public GameObject Turret;
    public float FireTime = 3;
    public bool ScatterFire = false;

    public GameObject blastPrefab; 
    public Transform Player; 

    void Start(){

            Turret = this.gameObject;

            Player = GameObject.FindWithTag("PlayerModel").transform;

        InvokeRepeating("fireBullets", 1.0f, FireTime);
        
    }

    private void Update()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y, Player.position.z + 20));
        if (ScatterFire)
        {
            transform.eulerAngles += new Vector3(Random.Range(-10,10), + Random.Range(-10, 10), + Random.Range(-10, 10));
        }
    }

    void fireBullets(){
        
        Instantiate(blastPrefab, transform.position, transform.rotation);
        Debug.Log("pew pew");
    }

    
}
