using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetSpin : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(0,720*Time.deltaTime,0);
    }
}
