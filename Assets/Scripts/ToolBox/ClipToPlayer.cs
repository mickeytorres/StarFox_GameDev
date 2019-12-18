using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipToPlayer : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }

        if (Player != null)
        {
            transform.position = Player.transform.position;
        }
    }
}
