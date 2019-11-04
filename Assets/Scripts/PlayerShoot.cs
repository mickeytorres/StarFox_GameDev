using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a user-controlled player character
//intent: press (w) to shoot at enemies

public class PlayerShoot : MonoBehaviour
{
    public GameObject blastPrefab; 
    public GameObject blastSpawn;

    bool canPress = true;
    float coolDown = 0.5f;

    public float damage = 1f;

    // Update is called once per frame
    void Update()
    {
        if (!canPress) {
            coolDown -= Time.deltaTime;
        }

        if (coolDown <= 0) {
            coolDown = 0.5f;
            canPress = true;
        }
        
        if(canPress) {
            Shoot();
        }
    }

    void Shoot() {
        if (Input.GetKey(KeyCode.Space) && canPress) {
            damage += 0.1f;
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            GameObject thisBlast = Instantiate(blastPrefab, blastSpawn.transform);
            thisBlast.GetComponent<BlastMovement>().damage = damage;
            damage = 1;
            canPress = false;
        }
    }
}

