using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //USAGE:
    //  Put this on the body of placeholder enemy's node (the inner one, the "EnemyNode").
    //  Set MyParent to Enemy outer layer.

    //PURPOSE:
    //  --Makes enemy delete itself after a few sec after its spawned
    //  --Makes enemy constantly look at player

    public float Z;
    public float X;

    public int Mode;
    public float timer = 10;
    public GameObject MyParent;
    public Transform Player;

    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(Player);
        timer -= Time.deltaTime;
    }
}
