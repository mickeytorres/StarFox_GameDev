﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

//USAGE: PUT ON ALL ASTEROIDS
//INTENT: ASTEROID MOVEMENT, ROTATION, HEALTH
public class AsteroidManager : MonoBehaviour
{

   private PlayerShoot _shot;
    
    private Rigidbody _rb;

    private MeshRenderer _mesh;
    //Prefabs
    [FormerlySerializedAs("_asteroidChunksPrefab")] public Rigidbody asteroidChunksPrefab;

    //Death variables
    private float _health = 6;
    
    //Movement  and rotation variables
    private float _speed = .5f;
    private float _tumble = 10;
   
    //Asteroid direction
    Vector3 direction = new Vector3(0f,0f,-1f);
        
    
    
    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
        AsteroidRotation();
        RandomTrajectory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = direction * _speed;
        
    }

    private void RandomTrajectory()
    {
        float randomNumX = Random.Range(-.3f,.3f);
        float randomNumY = Random.Range(-.3f, .3f);
        
        direction += new Vector3(randomNumX,randomNumY,0f) ;

        direction = direction.normalized;
    }

    private void AsteroidRotation()
    {
        _rb.angularVelocity = Random.insideUnitSphere * _tumble;
    }

    public void OnTriggerEnter(Collider other)
    {
        _shot = other.transform.GetComponent<PlayerShoot>();
        DamageCalculator();
    }

    private void DamageCalculator()
    {
        if (_shot.singlePrefab)
        {
            _health -= _shot.singleDamage;
        }
        else if (_shot.doubleGPrefab)
        {
            _health -= _shot.doubleGDamage;
        }
        else if (_shot.doubleBPrefab)
        {
            _health -= _shot.doubleBDamage;
        }
        else if (_shot.chargedPrefab)
        {
            _health -= _shot.chargedDamage;
        }
        else if (_shot.bombPrefab)
        {
            _health -= _shot.bombDamage;
        }
    }

    private void Explosion()
    {
        if (_health <= 0)
        {
            _mesh.enabled = false;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(asteroidChunksPrefab, transform.position, Quaternion.identity);

                asteroidChunksPrefab.velocity = direction * 2;

            }
           

        }
    }




}
