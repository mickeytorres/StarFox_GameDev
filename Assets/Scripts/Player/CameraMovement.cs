using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //USAGE:
    //  Put this script on the Camrea.
    //  Set "PlaneTransform" as "Plane"
    //  Set "player" as player (the most outer layer).
    //  Set "Gyroscope" as plane's Gyroscope.

    //PURPOSE:
    //  --Makes the camera move and rotate as Plane moves.
    //  --Makes the camera shake up and down.

    public Transform PlaneTransform;
    public Transform PlaneBody;
    public GameObject player;
    public Transform Gyroscope;

    //The slight addition to X rotation to make the screen shake up and down.
    //The shake follows the pattern of f(x) = Sin(x), therefore a timer for "x" is needed.
    public float shaker;
    public float timer;

    //The camrea isn't going to move as much as the actual plane, here's their ratio.
    public float XRatio;
    public float YRatio;

    //Position of the plane
    public float XPos;
    public float YPos;
    public float ZPos;

    public float normalCameraZDist = -5f;
    public float boostCameraZDist = -8f;
    public float brakeCameraZDist = -1;

    public float zMovementTimer;
    public float timeForZMovementSpeed = 1f;

    //Rotation of Gyroscope (so its independent from the barrelrolling of the plane)
    public float XRotation;
    public float YRotation;
    public float ZRotation;

    public static CameraMovement instance;

    private void Awake()
    {
        instance = this;
    }

    //Resets shaker and timer
    void Start()
    {
        shaker = 0;
        timer = 0;
    }

    void Update()
    {
        //Shakes the screen up and down slowly.
        timer += Time.deltaTime * 3;
        shaker = 0.5f * Mathf.Sin(timer);

        //Set XY displacement of camera to a portion of the planes's.
        //Both GameObjects has their transform isolated from world position
        //therefore their localposition should both be displacement from 0,0,0.
        XPos = PlaneTransform.localPosition.x * XRatio;
        YPos = (PlaneTransform.localPosition.y + PlaneBody.localPosition.y)* YRatio;

        //Set the rotation of camrea to a portion of of the Gyroscope's.
        //Positive rotations over 180 degree will be set to negative numbers to make sure the
        //multiplyers work correctly
        XRotation = Gyroscope.localEulerAngles.x;
        if (XRotation > 180)
        {
            XRotation -= 360;      
        }
        XRotation *= 0.2f;

        YRotation = Gyroscope.localEulerAngles.y;
        if (YRotation > 180)
        {
            YRotation -= 360;
        }
        YRotation *= 0.2f;

        if (Forward.instance.boost || Forward.instance.brake)
        {
            zMovementTimer += Time.deltaTime;
            var t = zMovementTimer / timeForZMovementSpeed;
            if (t > 1)
            {
                t = 1;
            }

            var finalDistance = 0f;
            if (Forward.instance.boost)
            {
                finalDistance = boostCameraZDist;
            }
            else
            {
                finalDistance = brakeCameraZDist;
            }

            ZPos = Mathf.Lerp(normalCameraZDist, finalDistance, t);
        }
        else
        {
            
            ZPos = Mathf.Lerp(ZPos, normalCameraZDist, 0.1f);
        }
        
        
        //Implements the variables.
        transform.localPosition = new Vector3(XPos, YPos + 1, ZPos);
        transform.localEulerAngles = new Vector3 (XRotation + shaker - 5, YRotation, YRotation * 0.5f);
    }
}
