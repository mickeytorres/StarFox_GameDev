using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Player;

    public GameObject FirstWaveButterfly;
    public bool Has_FirstWaveButterfly_Spawned = false;

    public GameObject Mech;
    public GameObject FirstWaveWorm;
    public GameObject ConeHeads;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z >= 20 && !Has_FirstWaveButterfly_Spawned)
        {
            Instantiate(FirstWaveButterfly);
            Has_FirstWaveButterfly_Spawned = true;
        }


    }
}
