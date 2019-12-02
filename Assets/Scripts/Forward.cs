using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//USAGE: PUT ON PLAYER PARENT OBJECT
//INTENT: CONTROLS MOVEMENT, BOOST, BRAKING, MANAGES HEALTH AND ENERGY;
public class Forward : MonoBehaviour
{
    public static Forward instance;

    public HealthManager energyBar;
    
    private Rigidbody _rb;

    //MOVEMENT VARIABLES
    public float _normalspeed;
    public float _boostspeed;
    public float _brakespeed;
    public float _speed;

    public bool boost = false;
    public bool brake = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if (_normalspeed == 0)
        {
            _normalspeed = 20;
        }
        if (_brakespeed == 0)
        {
            _brakespeed = 10;
        }
        if (_boostspeed == 0)
        {
            _boostspeed = 50;
        }
        instance = this;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Boost();
        Brake();
        if (PlayerPlaneMovement.instance.Somersaulting)
        {
            _speed = 0f;
        }
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
            _speed = _boostspeed;
            Debug.Log(_speed);
            
        }
        else
        {
            _speed = _normalspeed;
            boost = false;
            PlayerPlaneMovement.instance.Boosting = false;
        }
    }

    public void Brake()
    {
        if (Input.GetKey(KeyCode.S))
        {
            brake = true;
            _speed = _brakespeed;
            PlayerPlaneMovement.instance.Braking = true;
        }
        else if (!boost)
        {
            _speed = _normalspeed;
            brake = false;
            PlayerPlaneMovement.instance.Braking = false;
        }
    }
}
