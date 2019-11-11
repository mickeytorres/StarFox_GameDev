using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float timer = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += 20 * transform.forward * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && timer > 0.1)
        {
            timer = 0.1f;
        }
    }
}
