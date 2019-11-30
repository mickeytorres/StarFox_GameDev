using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//USAGE: PUT ON ALL ASTEROIDS
//INTENT: ASTEROID MOVEMENT, ROTATION, HEALTH

public class Enemy_HealthManager : MonoBehaviour
{
    private BlastMovement _shot;
    
    private Rigidbody _rb;

    private MeshRenderer _mesh;
    public GameObject ExplosionPrefab;

    //Death variables
    public float _health = 6;

    public bool inbombRange; 

    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //detects what laser type and damage amount 
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blast"))
        {
            _shot = other.transform.GetComponent<BlastMovement>();
            _health -= _shot.damage;
            if (!_shot.IsABombExplosion)
            {
                Destroy(_shot.gameObject);
            }
            Explosion();
            //DamageCalculator();
        }
    }

    //Destroys object and instantiates 3 smaller objects
    private void Explosion()
    {
        if (_health <= 0)
        {
          //  _mesh.enabled = false;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

                //asteroidChunksPrefab.GetComponent<Rigidbody>().velocity = direction * 2;

            }
            Destroy(gameObject);

        }
       
    }
    

}
