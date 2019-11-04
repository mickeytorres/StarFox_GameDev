using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controls the movement of scope which the plane then follows

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

            if (MyPlane.transform.eulerAngles.z > 310)
            {
                Plane.instance.IsMovingHorizontally = true;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * MoveSpeed * Time.deltaTime;

            if (MyPlane.transform.eulerAngles.z < 50)
            {
                Plane.instance.IsMovingHorizontally = true;
            }
        }
        else
        {
            Plane.instance.IsMovingHorizontally = false;
        }

    }
}
