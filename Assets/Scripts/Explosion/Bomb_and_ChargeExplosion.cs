using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_and_ChargeExplosion : MonoBehaviour
{
    public float damage = 10;
    public GameObject effect;

    void Awake()
    {
        Instantiate(effect, transform.position, transform.rotation);
        this.GetComponent<BlastMovement>().damage = damage;
    }


    void FixedUpdate()
    {
        
    }
}
