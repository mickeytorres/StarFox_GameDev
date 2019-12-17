using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : MonoBehaviour
{
    private GameObject Player;

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
        transform.LookAt(Player.transform);
        transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y,0);
    }
}
