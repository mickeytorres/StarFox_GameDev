using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controls the movement of scope which the plane then follows

public class PlayerNodeMovement : MonoBehaviour
{
    //USAGE:
    //  Put this script on whatever that makes the plane system move in the Player system.
    //  For now its "Plane"
    //  Set "MyPlane" as "Plane"
    //  Set "MyPlaneRigidbody" as "Plane" (Not used yet)

    //PURPOSE:
    //  --Makes the plane move UP and DOWN, LEFT and RIGHT in the player system
    //  (Independent from the world transform of the player system)


    public GameObject MyPlane;
    public Rigidbody MyPlaneRigidbody; 
    public float MoveSpeed;
    public float YLimit;
    public float XLimit;

    //Sets if the vertical control is reversed
    // 1 for yes
    // -1 for no
    public float VerticalControlReverser = 1;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //Move the plane Up and Down (stops it if both keys are pressed)
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && transform.localPosition.y <= YLimit)
        {
            transform.localPosition += transform.up * VerticalControlReverser * MoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && transform.localPosition.y >= -YLimit && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.localPosition -= transform.up * VerticalControlReverser * MoveSpeed * Time.deltaTime;
        }

        //Move the plane Left and Right (stops it if both keys are pressed)
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && transform.localPosition.x >= -XLimit)
        {
            transform.localPosition -= transform.right * MoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && transform.localPosition.x <= XLimit && !Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition += transform.right * MoveSpeed * Time.deltaTime;
        }       
    }
}
