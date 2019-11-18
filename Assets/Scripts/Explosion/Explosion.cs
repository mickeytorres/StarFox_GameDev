using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Explosion : MonoBehaviour
{
    //WARNING:
    //This script is not currently used anymore.
    //This is a port of Leo's Bike game's explosion function of cars where cars shreds to pieces of cubes after collision.

    public Transform RandomTransform;
    public float timer;

    void Start()
    {
        timer = 1.2f;

        for (int i = 30 - 1; i >= 0; i--)
        {
            RandomTransform = this.transform;
            RandomTransform.eulerAngles = new Vector3(Random.Range(0.0f, 360f), Random.Range(0.0f, 360f), Random.Range(0.0f, 360f));

            RandomTransform.position += transform.forward * 0.2f;
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
