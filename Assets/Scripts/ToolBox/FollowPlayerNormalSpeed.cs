using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerNormalSpeed : MonoBehaviour
{
    private float speed;
    public GameObject Player;

    void Start()
    {
        speed = Forward.instance._normalspeed;
        Player = Player = GameObject.FindWithTag("Player");
        transform.position = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime; 
    }
}
