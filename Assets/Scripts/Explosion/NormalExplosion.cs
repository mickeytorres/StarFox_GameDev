using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalExplosion : MonoBehaviour
{
    private float timer = 0;
    private float liltimer = 0;
    private int Counter = 0;
    public GameObject Flame;

    void Update()
    {
        timer += Time.deltaTime;
        liltimer -= Time.deltaTime;
        if (liltimer <= 0 && Counter < 3)
        {
            Instantiate(Flame);
            Counter++;
            liltimer = 0.1f;
        }

        if (timer > 2.4f)
        {
            Destroy(this.gameObject);
        }
    }
}
