using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidScope : MonoBehaviour
{
    //USAGE:
    //  Put this script on the child of Scopes.

    //PURPOSE:
    //  Makes the scope constantly lookforward while being at where it should be.

    public GameObject Midscope;

    void Update()
    {
        transform.position = Midscope.transform.position;
    }
}
