using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    public float timer = 0;
    public Transform Player;
    public GameObject Bullet;

    void Update()
    {
        transform.LookAt(Player);
        timer += Time.deltaTime;
        if (timer > 4)
        {
            Instantiate(Bullet,transform.position,transform.rotation);
            timer = 0;
        }
    }
}
