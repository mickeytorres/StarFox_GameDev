using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipDebug : MonoBehaviour
{
    public float SomersaultFlipAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPlaneMovement.instance.transform.localEulerAngles.y == 180)
        {
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
