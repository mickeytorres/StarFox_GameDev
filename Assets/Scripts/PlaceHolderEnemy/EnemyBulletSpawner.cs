using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    //USAGE:
    //  Put this script on Temperary Enemy's BulletSpawner.
    //  Set PlaneBody as Player;
    //  Set chosen bullet as Bullet;
    //  Set the transform of the enemy gameObject's this is supposed to be attached to as MyParent.(i.e. Who's "holding the gun")

    //PURPOSE:
    //  --Acts as a "Gun" that constantly points at the player and fires a bullet once every 4 second

    public float timer = 0;
    public Transform Player;
    public GameObject Bullet;
    public Transform MyParent;

    void Update()
    {
        transform.LookAt(Player);
        timer += Time.deltaTime;
        if (timer > 4)
        {
            Instantiate(Bullet,transform.position,transform.rotation,MyParent);
            timer = 0;
        }
    }
}
