using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidScope : MonoBehaviour
{
    //Controls the movement of scope between the further scope control and player.
    public GameObject Midscope;

    void Update()
    {
        transform.position = Midscope.transform.position;
    }
}
