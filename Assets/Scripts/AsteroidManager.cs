using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidManager : MonoBehaviour
{
    private Rigidbody _rb;

    private float _health = 6;
    private float tumble = 10;
   
    
    Vector3 direction = new Vector3(0f,0f,-1f);
        
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        AsteroidRotation();
        RandomTrajectory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomTrajectory()
    {
        float randomNum = Random.Range(-.3f,.3f );
        
        direction += new Vector3(randomNum,randomNum,-1f);

        direction = direction.normalized;
    }

    private void AsteroidRotation()
    {
        _rb.angularVelocity = Random.insideUnitSphere * tumble;
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
