using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDebug : MonoBehaviour
{
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
