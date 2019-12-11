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
    public float maxSomersaultHeight;

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
    public float InputComboTimeLeft;
    //  What was the last input.
    public string LoadedInput;
    
    //Times the rolling
    public float RollTimer;

    //Timers the somersault
    public float SomersaultTimer;
    public float SomersaultFlipAngle;
    public GameObject Model;

    void Awake()
    {
        instance = this;
    }

    //Reset status.
    void Start()
    {
        FreeToMove();
        InputComboTimeLeft = 0;
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
    public bool CanFreeTilt()
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
        if (!Somersaulting)
        {
            transform.localPosition += (new Vector3(0, 0, 0) - transform.localPosition) * 0.1f;
            if (Mathf.Abs(transform.localPosition.z) < 0.01f)
            {
                transform.localPosition = new Vector3(0, 0, 0);
            }
        }


        //Timer for InputBuffer, clears LoadedInput if time goes out
        if (InputComboTimeLeft > 0)
        {
            InputComboTimeLeft -= Time.deltaTime;
        }
        else
        {
            InputComboTimeLeft = 0;
            LoadedInput = null;
        }

        //Barrel Rolling
        if (RollTimer > 0)
        {
            if (RollingL)
            {
                Rotation += RotateSpeed * 300 * Time.deltaTime;
                Debug.Log("Rolling");
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
                Rotation -= RotateSpeed * 300 * Time.deltaTime;
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
                    InputComboTimeLeft = 0.5f;
                    LoadedInput = "TiltingL";
                }
                else if (LoadedInput == "TiltingL" && InputComboTimeLeft > 0)
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
                    InputComboTimeLeft = 0.5f;
                    LoadedInput = "TiltingR";
                }
                else if (LoadedInput == "TiltingR" && InputComboTimeLeft > 0)
                {
                    RollingR = true;
                    RollTimer = 0.5f;
                }
            }

            //Initiates test for combo tapping to somersault
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (LoadedInput == "Boost" && InputComboTimeLeft > 0)
                {
                    Somersaulting = true;
                    SomersaultTimer = 3f;
                    SomersaultFlipAngle = 0;
                }
                else if (LoadedInput != "DownArrow")
                {
                    InputComboTimeLeft = 0.5f;
                    LoadedInput = "DownArrow";
                    SomersaultFlipAngle = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (LoadedInput == "DownArrow" && InputComboTimeLeft > 0)
                {
                    Somersaulting = true;
                    SomersaultTimer = 3f;
                }
                else if (LoadedInput != "Boost")
                {
                    InputComboTimeLeft = 0.5f;
                    LoadedInput = "Boost";
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

            
            
        }

        //Somersaulting
        if (Somersaulting)
        {
            SomersaultTimer -= Time.deltaTime;
            if (SomersaultTimer > 0)
            {
                
                GyroScope.instance.RotationGoalX = 0;
                GyroScope.instance.RotationGoalY = 0;
                RotationGoal = 0;

                SomersaultFlipAngle -= 120 * Time.deltaTime;
                //transform.localEulerAngles = new Vector3(SomersaultFlipAngle, 0, 0);

                
                // Dec/9/19  commented lines out for debug purposes - george 
                transform.Rotate(new Vector3(-120f, 0f, -0f) * Time.deltaTime);
                if (SomersaultTimer > 1.5)
                {
                    transform.localPosition =  Vector3.MoveTowards(transform.localPosition, new Vector3(0f,maxSomersaultHeight,2f), Time.deltaTime * maxSomersaultHeight/1.5f);
                }
                else if (SomersaultTimer < 1.5)
                {
                    transform.localPosition =  Vector3.MoveTowards(transform.localPosition, new Vector3(0f,0f,0f), Time.deltaTime * maxSomersaultHeight/1.5f);
                }

                // transform.RotateAround(Forward.instance.gameObject.transform.position, new Vector3 (0,0,0), SomersaultFlipAngle);
                //transform.localPosition += transform.forward * Forward.instance._boostspeed * Time.deltaTime * (1 + 0.05f * (3 - SomersaultTimer));
                /*
                if (transform.eulerAngles.y == 180)
                {
                    Model.transform.localEulerAngles = new Vector3(0, 0, 180);
                }
                else
                {
                    Model.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                */

            }
            else
            {
             transform.localEulerAngles = new Vector3(0, 0, 0);
             transform.localPosition = new Vector3(0, 0, 0);
                Somersaulting = false;
            }

            


        }

        //Rotation Goal System for Z rotation
        //Input.GetAccess is better
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Somersaulting)
            {
                if (CanFreeTilt())
                {
                    RotationGoal = -30;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Somersaulting)
            {
                if (CanFreeTilt())
                {
                    RotationGoal = 30;
                }
            }
            else
            {
                if (CanFreeTilt())
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

            //If somersaulting allow full rotation 
            if (!Somersaulting)
            {
                //Implements variables.
                transform.localEulerAngles =
                    new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Rotation);
            }
    }

        IEnumerator Somersault()
        {
        yield return new WaitForSeconds(3f);
        }
}

    

