﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNode : MonoBehaviour
{
    public GameObject Player;
     private BlastMovement _shot;

    private float _health = 1f;
    
    Vector3 direction = new Vector3(0f,0f,-1f);

    public GameObject asteroidChunksPrefab;

    private void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blast"))
        {
            _shot = other.transform.GetComponent<BlastMovement>();
            _health -= _shot.damage;
            Explosion();
        }
    }
    private void RandomTrajectory()
    {
        float randomNumX = Random.Range(-.3f,.3f);
        float randomNumY = Random.Range(-.3f, .3f);
        
        direction += new Vector3(randomNumX,randomNumY,0f) ;

        direction = direction.normalized;
    }
    private void Explosion()
    {
        if (_health <= 0)
        {
            //  _mesh.enabled = false;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(asteroidChunksPrefab, transform.position, Quaternion.identity);

                asteroidChunksPrefab.GetComponent<Rigidbody>().velocity = direction * 2;

            }
            Destroy(gameObject);

        }
       
    }
}
