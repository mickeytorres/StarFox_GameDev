using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNode : MonoBehaviour
{
    //USAGE:
    //  Put this script on the Camrea Node (parent of Camrea).
    //  Set Player (the most outer layer with script "Foward") gameObject as "player"

    //PURPOSE:
    //  --Makes the camera's node follow player;

    public Transform player;

    void Update()
    {
        transform.position = player.transform.position;
    }
}
