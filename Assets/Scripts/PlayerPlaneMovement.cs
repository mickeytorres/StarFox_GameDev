using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaneMovement : MonoBehaviour
{
    public static PlayerPlaneMovement instance;

    public float RetractSpeed;
    public float RotateSpeed;
    public float Rotation;
    public bool TiltingL;
    public bool TiltingR;
    public bool Boosting;
    public bool Braking;
    public bool RollingL;
    public bool RollingR;
    public bool UTurning;
    public bool Somersaulting;
    public bool Occupied;

    void Awake()
    {

        instance = this;
    }

    void Start()
    {
        FreeToMove();
    }

    public void FreeToMove()
    {
        TiltingL = false;
        TiltingR = false;
        Boosting = false;
        Braking = false;
        RollingL = false;
        RollingR = false;
        UTurning = false;
        Somersaulting = false;
        Occupied = false;
    }

    public bool FreeTilt()
    {
        if (!RollingL && !RollingR && !TiltingL && !TiltingR && !UTurning && !Somersaulting)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 

    void Update()
    {
        if (!UTurning && !Somersaulting)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (FreeTilt())
                {
                    if (Rotation < -30)
                    {
                        Rotation = -30;
                    }
                }
                Rotation -= RotateSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (FreeTilt())
                {
                    if (Rotation > 30)
                    {
                        Rotation = 30;
                    }
                }
                Rotation += RotateSpeed * Time.deltaTime;
            }
            else if (Rotation > 0 && Rotation <= 180)
            {
                Rotation -= RetractSpeed * Time.deltaTime;
                if (Rotation < 0)
                {
                    Rotation = 0;
                }
            }
            else if (Rotation < 0 && Rotation >= -180)
            {
                Rotation += RetractSpeed * Time.deltaTime;
                if (Rotation > 0)
                {
                    Rotation = 0;
                }
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Q) && !Occupied)
        {
            Occupied = true;
            TiltingL = true;
        }
        if (Input.GetKey(KeyCode.Q) && Occupied == true && TiltingL == true)
        {
            Occupied = true;
            TiltingL = true;
        }
        else
        {
            Occupied = false;
            TiltingL = false;
        }


        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Rotation);
    }
}
