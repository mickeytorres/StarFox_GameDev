using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public Image healthBar;
    public Image energyBar;

    public Forward player;

    private float _energy = 50f;
    private float _health = 100f;
    private float _maxEnergy = 50f;
    private float _maxHealth = 100f;


    public bool canBoost = true;


    private void Awake()
    {
        instance = this;
    }

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
        if(other.gameObject.CompareTag("EnemyBlast"))
        {
            _health -= 7f;
        }
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