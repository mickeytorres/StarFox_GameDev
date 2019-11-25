using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Player;

    public GameObject FirstWaveButterfly;
    public float FirstWaveButterfly_SpawnZpos;
    public bool Has_FirstWaveButterfly_Spawned = false;

    public GameObject Mech;
    public float Mech_SpawnZpos;
    public bool Has_Mech_Spawned = false;

    public GameObject FirstWaveWorm;
    public float FirstWaveWorm_SpawnZpos;
    public bool Has_FirstWaveWorm_Spawned = false;

    public GameObject ConeHeads;
    public float ConeHeads_SpawnZpos;
    public bool Has_ConeHeads_Spawned = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z >= FirstWaveButterfly_SpawnZpos && !Has_FirstWaveButterfly_Spawned)
        {
            Instantiate(FirstWaveButterfly);
            Has_FirstWaveButterfly_Spawned = true;
        }


    }
}
