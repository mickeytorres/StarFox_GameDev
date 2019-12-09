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

    void Start(){

            Turret = this.gameObject;


            Player = GameObject.FindWithTag("PlayerModel").transform;

        InvokeRepeating("fireBullets", 1.0f, 3f);
        
    }

    void Update()
    {
        
    }

    void fireBullets(){
        transform.LookAt(new Vector3(Player.transform.position.x,Player.transform.position.y,Player.position.z + Forward.instance._speed));
        Instantiate(blastPrefab, Turret.transform.position, Turret.transform.rotation);
        Debug.Log("pew pew");
    }

    
}
