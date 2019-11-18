using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    //WARNING: 
    //  This script isn't used anymore.

    //USAGE:
    //  Put this script on "Plane".And set MyRigidbody to itself.

    //PURPOSE:
    //
    //  --Restore it's Z rotaion with Rigidbody.(Not used anymore)
    //  --Detect if the plane is moving horizontally.

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
        if (!(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
        {
            Z_Correction();
        }
        */
    }

    public void Z_Correction()
    {
        /*
                if (transform.eulerAngles.z >= 1 && transform.eulerAngles.z <= 180)
        {
            MyRigidbody.AddTorque(transform.forward * -AutoCorrectTorque * Time.deltaTime);
        }
        if (transform.eulerAngles.z < 359 && transform.eulerAngles.z > 180)
        {
            MyRigidbody.AddTorque(transform.forward * +AutoCorrectTorque * Time.deltaTime);
        }
        */
    }
}
