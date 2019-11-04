using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidScope : MonoBehaviour
{
    public GameObject Midscope;

    void Update()
    {
        transform.position = Midscope.transform.position;
    }
}
