using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTrap : MonoBehaviour
{
   
   public Transform center;

   private float playerZPos;
   


   // Start is called before the first frame update
    void Start()
    {
       Vector3 target = center.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTrap()
    {
       transform.position  = Vector3.MoveTowards(transform.position,center.position,Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
       ActivateTrap();
    }
}
