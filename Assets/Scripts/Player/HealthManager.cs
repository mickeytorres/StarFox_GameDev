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

    public Renderer playerRenderer;
    public Color lerpedColor = Color.red;
    public Color currentColor;

    public Forward player;

    public static int PlayerLives = 3;

    //health and damage variables
    private float _energy = 50f;
    private float _health = 42f;
    private float _maxEnergy = 50f;
    private float _maxHealth = 42f;
    private float invincibilityTime = 0f;
    private float colorFlashTimer;
    private float colorFlashDuration = 3f;
    private float lerpSpeed = 30f;
    public bool shouldBeFlashing;

    public bool canBoost = true;

    private bool justLost = false;


    private Camera mainCamera;
    public AudioSource hitSound;

    void Start() {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        //this if statement is all just for testing, it will be removed soon! 
        // if (Input.GetKeyDown(KeyCode.G)) {
        //     PlayerLives -= 1;
        //     if (PlayerLives > 0) {
        //         SceneManager.LoadScene(1);
        //     } 
        // }

        energyBar.fillAmount = _energy / _maxEnergy;
        healthBar.fillAmount = _health / _maxHealth;
        DrainEnergy();
        if (shouldBeFlashing) {
            ColorFlash();
            Debug.Log("flash");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Bomb" && other.gameObject.tag != "Blast" && other.gameObject.tag != "ShootPowerup"
        && other.gameObject.tag != "BombPowerup" && other.gameObject.tag != "RingPowerup") {
            hitSound.Play();
            StartCoroutine(mainCamera.GetComponent<ScreenShake>().Shake(0.1f, 0.5f));
            if (Time.time < invincibilityTime) {
                return;
            }
            else {
                _health -= 7f;
                invincibilityTime = Time.time + 3;
                shouldBeFlashing = true;
                colorFlashTimer = 0;
            }
            
            if (_health <= 0 && PlayerLives > 0) {
                
                PlayerLives--;
                _health = _maxHealth;
                SceneManager.LoadScene(1);
            }
        }

        if (other.gameObject.tag == "RingPowerup") {
            _health = _maxHealth;
        }
    }

    public void ColorFlash()
    {
        colorFlashTimer += Time.deltaTime;
        
        Material[] allMaterials = playerRenderer.materials;

        if (colorFlashTimer < colorFlashDuration)
        {
            Debug.Log(colorFlashTimer);
            for (int i = 0; i < allMaterials.Length; i++)
            {
                 allMaterials[i].SetColor("_EmissionColor",Color.Lerp(currentColor, lerpedColor, Mathf.Sin(colorFlashTimer*lerpSpeed)));

            }
        }
        else
        {
            for (int i = 0; i < allMaterials.Length; i++)
            {
                allMaterials[i].SetColor("_EmissionColor", currentColor);
            }
            shouldBeFlashing = false;
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