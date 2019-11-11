using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Assign this to PlaneBody;
    public Transform PlaneTransform;

    public float shaker;
    public float XRatio;
    public float YRatio;
    public float XPos;
    public float YPos;

    public GameObject player;


    void Start()
    {
        shaker = 0;
    }

    // Update is called once per frame
    void Update()
    {
        XPos = PlaneTransform.localPosition.x * XRatio;
        YPos = PlaneTransform.localPosition.y * YRatio;
        transform.localPosition = new Vector3(XPos, YPos,-7f);
    }
}
