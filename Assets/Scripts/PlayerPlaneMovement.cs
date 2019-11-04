using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controls the tilt, rolling, boost, brake, and Sumersalt

public class PlayerPlaneMovement : MonoBehaviour
{
    public static PlayerPlaneMovement instance;

    public float RetractSpeed;
    public float RotateSpeed;
    public float Rotation;
    public float RotationGoal;
    public bool TiltingL;
    public bool TiltingR;
    public bool Boosting;
    public bool Braking;
    public bool RollingL;
    public bool RollingR;
    public bool UTurning;
    public bool Somersaulting;
    public bool Occupied;

    public float InputBuffer;
    public string LoadedInput;
    public float RollTimer;

    void Awake()
    {

        instance = this;
    }

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


        if (InputBuffer > 0)
        {
            InputBuffer -= Time.deltaTime;
        }
        else
        {
            InputBuffer = 0;
            LoadedInput = null;
        }

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


        if (!RollingL && !RollingR && !UTurning && !Somersaulting)
        {
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

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (FreeTilt())
                {
                    RotationGoal = -30;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
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

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Rotation);
        }
    }
}
