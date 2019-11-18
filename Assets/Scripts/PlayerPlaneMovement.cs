using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controls the tilt, rolling, boost, brake, and Sumersalt

public class PlayerPlaneMovement : MonoBehaviour
{
    //USAGE:
    //  Put this script on whatever that makes the plane roll and tilt. For now it is "PlaneBody"

    //PURPOSE:
    //  --Makes the plane naturally tilt towards moving direction.
    //  --Also detects and controls if the plane is:
    //      90degree tilting, 
    //      barrel-rolling,
    //      boosting,
    //      brake,
    //      Sumersalt,
    //  (Independent from the world transform of the player system, everything starts from 0,0,0)

    public static PlayerPlaneMovement instance;

    //Uses RotationGoal System: 
    //Player inputs sets Rotationgoal for object, then the object would rotate towards goal constantly.
    public float RetractSpeed;
    public float RotateSpeed;
    public float Rotation;
    public float RotationGoal;

    //list of bools that restricts player from performing certain acts according to what the plane is currently doing.
    public bool TiltingL;
    public bool TiltingR;
    public bool Boosting;
    public bool Braking;
    public bool RollingL;
    public bool RollingR;
    public bool UTurning;
    public bool Somersaulting;
    public bool Occupied;

    //Detects double tapping and combo tapping
    //  For how long would the system wait for the next input.
    public float InputBuffer;
    //  What was the last input.
    public string LoadedInput;
    
    //Times the rolling
    public float RollTimer;

    void Awake()
    {
        instance = this;
    }

    //Reset status.
    void Start()
    {
        FreeToMove();
        InputBuffer = 0;
        RollTimer = 0;
    }

    //Clears all restrictons
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

    //Bool checking if the plane is free to tilt by turning (or auto correction)
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
        //Timer for InputBuffer, clears LoadedInput if time goes out
        if (InputBuffer > 0)
        {
            InputBuffer -= Time.deltaTime;
        }
        else
        {
            InputBuffer = 0;
            LoadedInput = null;
        }

        //Barrel Rolling
        if (RollTimer > 0)
        {
            if (RollingL)
            {
                Rotation += RotateSpeed * 50 * Time.deltaTime;
                RollTimer -= Time.deltaTime;
                if (RollTimer <= 0)
                {
                    RollTimer = 0;
                    RollingL = false;
                    LoadedInput = "Null";
                    Rotation = transform.localEulerAngles.z - 360;
                }
            }
            else if (RollingR)
            {
                Rotation -= RotateSpeed * 50 * Time.deltaTime;
                RollTimer -= Time.deltaTime;
                if (RollTimer <= 0)
                {
                    RollTimer = 0;
                    RollingR = false;
                    LoadedInput = "Null";
                    Rotation = transform.localEulerAngles.z;
                }
            }
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Rotation);
        }

        //Input controls
        if (!RollingL && !RollingR && !UTurning && !Somersaulting)
        {
            //Initiates test for double tapping to barrel rolling left
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (LoadedInput != "TiltingL")
                {
                    InputBuffer = 0.5f;
                    LoadedInput = "TiltingL";
                }
                else if (LoadedInput == "TiltingL" && InputBuffer > 0)
                {
                    RollingL = true;
                    RollTimer = 0.5f;
                }
            }

            //Initiates test for double tapping to barrel rolling right
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (LoadedInput != "TiltingR")
                {
                    InputBuffer = 0.5f;
                    LoadedInput = "TiltingR";
                }
                else if (LoadedInput == "TiltingR" && InputBuffer > 0)
                {
                    RollingR = true;
                    RollTimer = 0.5f;
                }
            }

            //90 degree tilting
            if (Input.GetKey(KeyCode.Q) && !TiltingR)
            {
                Occupied = true;
                TiltingR = false;
                TiltingL = true;
                RotationGoal = 90;
            }
            else if (Input.GetKey(KeyCode.E) && !TiltingL)
            {
                Occupied = true;
                TiltingL = false;
                TiltingR = true;
                RotationGoal = -90;
            }
            else
            {
                Occupied = false;
                TiltingL = false;
                TiltingR = false;
            }

            //Rotation Goal System for Z rotation
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                if (FreeTilt())
                {
                    RotationGoal = -30;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                if (FreeTilt())
                {
                    RotationGoal = 30;
                }
            }
            else
            {
                if (FreeTilt())
                {
                    RotationGoal = 0;
                }
            }

            //Corrections for over-rotating.
            if (Rotation > RotationGoal)
            {
                Rotation -= Mathf.Abs(RotationGoal - Rotation) * RotateSpeed * Time.deltaTime;
                if (Rotation <= RotationGoal)
                {
                    Rotation = RotationGoal;
                }
            }
            else if (Rotation < RotationGoal)
            {
                Rotation += Mathf.Abs(RotationGoal - Rotation) * RotateSpeed * Time.deltaTime;
                if (Rotation >= RotationGoal)
                {
                    Rotation = RotationGoal;
                }
            }

            //Implements variables.
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Rotation);
        }
    }
}
