using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour
{
    private Rigidbody _rb;

    private float _speed = 10;
    private bool boost = false;
        
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Boost();
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
        if (Input.GetKey(KeyCode.W))
        {
            boost = true;
            _speed = 30;
        }
        else
        {
            _speed = 10;
        }
    }
}
