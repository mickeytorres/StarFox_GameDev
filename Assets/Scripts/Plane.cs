using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Rigidbody MyRigidbody;
    public float AutoCorrectTorque;
    public bool IsMovingHorizontally;
    public static Plane instance;
  

    //Attempted code to tilt

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        /*
        if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            Z_Correction();
        }
        */
       
    }



    public void Z_Correction()
    {
        /*
        while (transform.eulerAngles.z > 360)
        {
            transform.eulerAngles -= new Vector3 (0,0,360);
        }
        while (transform.eulerAngles.z < 0)
        {
            transform.eulerAngles += new Vector3(0, 0, 360);
        }
        */

        if (transform.eulerAngles.z >= 1 && transform.eulerAngles.z <= 180)
        {
            MyRigidbody.AddTorque(transform.forward * -AutoCorrectTorque * Time.deltaTime);
        }
        if (transform.eulerAngles.z < 359 && transform.eulerAngles.z > 180)
        {
            MyRigidbody.AddTorque(transform.forward * +AutoCorrectTorque * Time.deltaTime);
        }
    }
}
