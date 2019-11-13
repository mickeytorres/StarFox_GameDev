using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Explosion : MonoBehaviour
{
    public GameObject Boxes;
    public Transform RandomTransform;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 1.2f;

        for (int i = 30 - 1; i >= 0; i--)
        {
            RandomTransform = this.transform;
            RandomTransform.eulerAngles = new Vector3(Random.Range(0.0f, 360f), Random.Range(0.0f, 360f), Random.Range(0.0f, 360f));

            RandomTransform.position += transform.forward * 0.2f;
            //Instantiate(Boxes, RandomTransform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
