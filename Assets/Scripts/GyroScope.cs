using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScope : MonoBehaviour
{
    public Transform scope;
    public float RotationY;
    public float RotationGoalY;
    public float RotationX;
    public float RotationGoalX;
    public float RotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        RotationY = 0;
        RotationGoalY = 0;
        RotationX = 0;
        RotationGoalX = 0;
        RotateSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotationGoalY = -45;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotationGoalY = 45;
        }
        else
        {
            RotationGoalY = 0;
        }
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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotationGoalX = 45;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            RotationGoalX = -45;
        }
        else
        {
            RotationGoalX = 0;
        }
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
        transform.localEulerAngles = new Vector3(RotationX, RotationY, transform.localEulerAngles.z);


    }
}
