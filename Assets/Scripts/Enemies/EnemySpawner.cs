using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject Player;

    //What am I spawning
    public GameObject SpawnWhat;

    //When do I spawn it according to player's z position
    public float SpawnZpos;

    //Have I spawned it
    public bool Has_Spawned = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z >= SpawnZpos && !Has_Spawned)
        {
            Instantiate(SpawnWhat);
            Has_Spawned = true;
        }
    }
}
