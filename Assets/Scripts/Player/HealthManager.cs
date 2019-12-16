using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{

    public Image healthBar;
    public Image energyBar;

    public Forward player;

    public static int PlayerLives = 3;

    private float _energy = 50f;
    private float _health = 100f;
    private float _maxEnergy = 50f;
    private float _maxHealth = 100f;

    public bool canBoost = true;

    private bool justLost = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            PlayerLives -= 1;
            SceneManager.LoadScene(1);
        }

        // if (Input.GetKeyDown(KeyCode.T)) {
        //     DecrementHealth();
        // }

        energyBar.fillAmount = _energy / _maxEnergy;
        healthBar.fillAmount = _health / _maxHealth;
        DrainEnergy();
        
    }

    //ignore this, it's just for testing
    // private void DecrementHealth() {
    //     _health -= 100;
        
    //     if (_health <= 0 && PlayerLives > 0) {
    //         PlayerLives -= 1;
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        _health -= 7f;

        if (_health <= 0 && PlayerLives > 0) {
            PlayerLives--;
            _health = 100;
            SceneManager.LoadScene(1);
        }
    }

    public bool LifeCheck() {
        //returns true as long as the player has lives left
        if (PlayerLives > 0) 
            return true;
        else   
            return false;
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
    
    public int GetLives() {
        return PlayerLives;
    }

    public void GameRestart() {
        PlayerLives = 3;
    }
}