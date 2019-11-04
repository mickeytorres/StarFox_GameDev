using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNodeMovement : MonoBehaviour
{
    public GameObject MyPlane;
    public Rigidbody MyPlaneRigidbody;
    public float VerticalControlReverser = 1;
    public float MoveSpeed;
    public float TorqueBase;
    public float TorqueOfHorizontalMovement;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += transform.up * VerticalControlReverser * MoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position -= transform.up * VerticalControlReverser * MoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * MoveSpeed * Time.deltaTime;
            /*
            if (MyPlane.transform.eulerAngles.z > 310 && MyPlane.transform.eulerAngles.z < 360)
            {
                TorqueOfHorizontalMovement = TorqueBase * (70 - MyPlane.transform.eulerAngles.z);
            }
            else
            {
                TorqueOfHorizontalMovement = 70;
            }
            */

            if (MyPlane.transform.eulerAngles.z > 310)
            {
                //MyPlaneRigidbody.AddTorque(transform.forward * -TorqueOfHorizontalMovement * Time.deltaTime);
                Plane.instance.IsMovingHorizontally = true;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            
            transform.position += transform.right * MoveSpeed * Time.deltaTime;
            /*
            if (MyPlane.transform.eulerAngles.z > 0 && MyPlane.transform.eulerAngles.z < 50)
            {
                TorqueOfHorizontalMovement = TorqueBase * (MyPlane.transform.eulerAngles.z - 290);
            }
            else
            {
                TorqueOfHorizontalMovement = 70;
            }
            */

            if (MyPlane.transform.eulerAngles.z < 50)
            {
               //MyPlaneRigidbody.AddTorque(transform.forward * TorqueOfHorizontalMovement * Time.deltaTime);
                Plane.instance.IsMovingHorizontally = true;
            }
        }
        else
        {
            Plane.instance.IsMovingHorizontally = false;
        }

    }
}
