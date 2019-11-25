using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScope : MonoBehaviour
{
    //USAGE:
    //  Put this script on the "GyroScope" of "Plane".

    //PURPOSE:
    //  --Makes player tilt toward the direction its moving towards.
    //  (Independent from the tilting of "PlaneBody")

    public static GyroScope instance;

    //Uses RotationGoal System: 
    //Player inputs sets Rotationgoal for object, then the object would rotate towards goal constantly.
    public float RotationY;
    public float RotationGoalY;
    public float RotationX;
    public float RotationGoalX;
    public float RotateSpeed;

    private void Awake()
    {
        instance = this;
    }

    //Resets all rotations.
    void Start()
    {
        RotationY = 0;
        RotationGoalY = 0;
        RotationX = 0;
        RotationGoalX = 0;
        RotateSpeed = 3f;
    }

    void Update()
    {
        //Rotation Goal System for Y rotation (left and right)
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !PlayerPlaneMovement.instance.Somersaulting)
        {
            RotationGoalY = -45;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !PlayerPlaneMovement.instance.Somersaulting)
        {
            RotationGoalY = 45;
        }
        else
        {
            //If player dosen't input anything, the plane slowly resets its rotation,
            //back to pointing forward.
            RotationGoalY = 0;
        }

        //Clips rotation back to RotationGoal to prevent overturning.
        if (RotationY > RotationGoalY)
        {
            RotationY -= Mathf.Abs(RotationGoalY - RotationY) * RotateSpeed * Time.deltaTime;
            if (RotationY <= RotationGoalY)
            {
                RotationY = RotationGoalY;
            }
        }
        else if (RotationY < RotationGoalY)
        {
            RotationY += Mathf.Abs(RotationGoalY - RotationY) * RotateSpeed * Time.deltaTime;
            if (RotationY >= RotationGoalY)
            {
                RotationY = RotationGoalY;
            }
        }

        //Rotation Goal System for X rotation (up and down)
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            RotationGoalX = 45;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            RotationGoalX = -45;
        }
        else
        {
            //If player dosen't input anything, the plane slowly resets its rotation,
            //back to pointing forward.
            RotationGoalX = 0;
        }

        //Clips rotation back to RotationGoal to prevent overturning.
        if (RotationX > RotationGoalX)
        {
            RotationX -= Mathf.Abs(RotationGoalX - RotationX) * RotateSpeed * Time.deltaTime;
            if (RotationX <= RotationGoalX)
            {
                RotationX = RotationGoalX;
            }
        }
        else if (RotationX < RotationGoalX)
        {
            RotationX += Mathf.Abs(RotationGoalX - RotationX) * RotateSpeed * Time.deltaTime;
            if (RotationX >= RotationGoalX)
            {
                RotationX = RotationGoalX;
            }
        }

        //Implements the rotations.
        transform.localEulerAngles = new Vector3(RotationX, RotationY, transform.localEulerAngles.z);


    }
}
