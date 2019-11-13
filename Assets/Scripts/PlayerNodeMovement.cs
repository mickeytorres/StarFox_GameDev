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
    public float YLimit;
    public float XLimit;

    void Start()
    {
        
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //transform.eulerAngles = new Vector3(0, transform.position.x, 0);

        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && transform.localPosition.y <= YLimit)
        {
            transform.localPosition += transform.up * VerticalControlReverser * MoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && transform.localPosition.y >= -YLimit && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.localPosition -= transform.up * VerticalControlReverser * MoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && transform.localPosition.x >= -XLimit)
        {
            transform.localPosition -= transform.right * MoveSpeed * Time.deltaTime;

            if (MyPlane.transform.eulerAngles.z > 310)
            {
                Plane.instance.IsMovingHorizontally = true;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && transform.localPosition.x <= XLimit && !Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition += transform.right * MoveSpeed * Time.deltaTime;

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
