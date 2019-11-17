using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDebug : MonoBehaviour
{
    //WARNING:
    //  This script only serves debug purpose.

    //USAGE:
    //  Put this script on an empty gameobject.
    //  Set "Explosion" as the prefab of explosion that you want to test

    //PURPOSE:


    public GameObject Explosion;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(Explosion);
        }
    }
}
