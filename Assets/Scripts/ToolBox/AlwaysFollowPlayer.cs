using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFollowPlayer : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
    }

    void Update()
    {
        this.transform.position = Player.transform.position;
    }
}
