using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //USAGE:
    //  Put this script on the bullet of our temperary enemy's bullet.

    //PURPOSE:
    //  Makes bullet move foward and disapper shortly after contact with player/upon the end of its life.

    public float timer = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moving foward
        transform.position += 20 * transform.forward * Time.deltaTime;

        //Timer for life time of bullet.
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroys itself shortly after collision, ensuring the player would have enough time to detect the contact.
        if (collision.gameObject.tag == "Player" && timer > 0.1)
        {
            timer = 0.1f;
        }
    }
}
