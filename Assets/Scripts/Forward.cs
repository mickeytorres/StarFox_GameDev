using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//USAGE: PUT ON PLAYER PARENT OBJECT
//INTENT: CONTROLS MOVEMENT, BOOST, BRAKING, MANAGES HEALTH AND ENERGY;
public class Forward : MonoBehaviour
{
    public HealthManager energyBar;
    
    private Rigidbody _rb;

    //MOVEMENT VARIABLES
    public float _speed;
    public bool boost = false;
    public bool brake = false;
        
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Boost();
        Brake();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _rb.velocity = transform.forward * _speed;
    }

    public void Boost()
    {
        if (Input.GetKey(KeyCode.W) && energyBar.canBoost)
        {
            
            boost = true;
            PlayerPlaneMovement.instance.Boosting = true;
            _speed = 30;
            Debug.Log(_speed);
            
        }
        else
        {
            _speed = 10;
            boost = false;
            PlayerPlaneMovement.instance.Boosting = false;
        }
    }

    public void Brake()
    {
        if (Input.GetKey(KeyCode.S))
        {
            brake = true;
            _speed = 5;
            PlayerPlaneMovement.instance.Braking = true;
        }
        else if (!boost)
        {
            _speed = 10;
            brake = false;
            PlayerPlaneMovement.instance.Braking = false;
        }
    }
}
