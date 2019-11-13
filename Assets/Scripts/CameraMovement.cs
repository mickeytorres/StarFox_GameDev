using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Assign this to PlaneBody;
    public Transform PlaneTransform;

    public float shaker;
    public float timer;

    public float XRatio;
    public float YRatio;
    public float XPos;
    public float YPos;
    public float ZPos;
    public float XRotation;
    public float YRotation;
    public float ZRotation;
    public GameObject player;
    public Transform Gyroscope;


    void Start()
    {
        shaker = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 3;
        shaker = 0.5f * Mathf.Sin(timer);

        XPos = PlaneTransform.localPosition.x * XRatio;
        YPos = PlaneTransform.localPosition.y * YRatio;

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

        transform.localPosition = new Vector3(XPos, YPos, ZPos);
        transform.localEulerAngles = new Vector3 (XRotation + shaker, YRotation, YRotation * 0.5f);
    }
}
