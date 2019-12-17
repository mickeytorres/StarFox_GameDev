using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleporter : MonoBehaviour
{
    //Press "T" to make player teleport to where this gameobject is. Makesure there's only one player and one teleporter.
    public GameObject Player;

    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Player.transform.position = this.transform.position;
        }
    }
}
