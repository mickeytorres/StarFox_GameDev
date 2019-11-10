using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public Image health;
    public Image energy;

    public Forward player;

    private float _energy = 50f;
    private float _health = 100f;
    private float _maxEnergy = 50f;
    private float _maxHealth = 100f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrainEnergy()
    {
        //if(player.boost==t)
    }
    
    
    
}
