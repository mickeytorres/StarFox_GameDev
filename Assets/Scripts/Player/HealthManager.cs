﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public Image healthBar;
    public Image energyBar;

    public Forward player;

    public int PlayerLives = 3;
    

    private float _energy = 50f;
    private float _health = 100f;
    private float _maxEnergy = 50f;
    private float _maxHealth = 100f;
    
    


    public bool canBoost = true;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        energyBar.fillAmount = _energy / _maxEnergy;
        healthBar.fillAmount = _health / _maxHealth;
        DrainEnergy();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _health -= 7f;
        if (_health <= 0)
        {
            PlayerLives--;
            _health = 100;
        }
        /*if(other.gameObject.CompareTag()
        {
           
        }*/
        
    }

    public void DrainEnergy()
    {
        if (player.boost && canBoost)
        {
            
            _energy -= 10f * Time.deltaTime;
   

        }

        if (_energy <= 0)
        {
            _energy = 0;
            canBoost = false;
        }

        if (player.boost == false && _energy < _maxEnergy)
        {
            _energy += 1f;
        }

        if (_energy >= 50f)
        {
            canBoost = true;


        }
    }
    
}