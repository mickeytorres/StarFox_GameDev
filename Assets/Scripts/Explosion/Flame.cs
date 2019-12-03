using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Flame : MonoBehaviour
{
    //USAGE:
    //  Put this script on explosion (or flames).

    //PURPOSE:
    //  --Makes a expanding fireball by expanding a sphere while rotating and shifting its texture.
    //  --Destroys itself after a few seconds.

    public bool Spin = true;
    public Vector3 SpinSpeed;
    public Renderer MyRenderer;
    Material m_Material;
    private float timer = 0;
    public float ScaleSpeed;
    public float LifeTime = 0.5f;

    //Initializes variables
    void Start()
    {
        if (Spin)
        {
            transform.eulerAngles = new Vector3(Random.Range(0.0f, 360f), Random.Range(0.0f, 360f), Random.Range(0.0f, 360f));
        }

        m_Material = GetComponent<Renderer>().material;

        //Randomizes spindirection && speed.
        if (Spin)
        {
            SpinSpeed = new Vector3(Random.Range(0.0f, 360f), Random.Range(0.0f, 360f), Random.Range(0.0f, 360f));
        }
        ScaleSpeed = 20;

        //Reinitialize the local scale
        transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        //Spinning the fireball
        if (Spin)
        {
            transform.eulerAngles += SpinSpeed * Time.deltaTime;
        }

        //Shifting the texture.
        m_Material.SetTextureOffset("_MainTex",new Vector2(1.42f, timer/0.7f));

        //Bloats the fireball according to ScaleSpeed
        transform.localScale += Time.deltaTime * new Vector3(ScaleSpeed, ScaleSpeed, ScaleSpeed);

        //Timer
        timer += Time.deltaTime;

        //ScaleSpeed slows down as fireball reaches the end of its life
        ScaleSpeed = 20/(1+(timer * timer));

        //Destroys the fireball after time runs out
        if (timer > LifeTime)
        {
            Destroy(this.gameObject); 
        }
    }
}
