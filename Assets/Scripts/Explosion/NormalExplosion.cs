using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalExplosion : MonoBehaviour
{
    //USAGE:
    //  Put this script on the "Explosion" Prefab or anything like it.
    //  Set Flame to desired prefab.

    //PURPOSE:
    //  --Instantiates 3 explosions (flames) quickly.

    private float timer = 0;
    private float liltimer = 0;
    private int Counter = 0;
    public GameObject Flame;

    void Update()
    {
        //Controls the lifetime of this gameObject
        timer += Time.deltaTime;

        if (timer > 2.4f)
        {
            Destroy(this.gameObject);
        }

        //Controls the time between explosions.
        liltimer -= Time.deltaTime;
        if (liltimer <= 0 && Counter < 3)
        {
            Instantiate(Flame,transform.position,transform.rotation);
            Counter++;
            liltimer = 0.1f;
        }
    }
}
