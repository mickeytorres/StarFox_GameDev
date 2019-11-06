using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Flame : MonoBehaviour
{
    public Vector3 SpinSpeed;
    public Renderer MyRenderer;
    Material m_Material;
    private float timer = 0;
    public float ScaleSpeed;

    void Start()
    {
        transform.eulerAngles = new Vector3(Random.Range(0.0f, 360f), Random.Range(0.0f, 360f), Random.Range(0.0f, 360f));
        m_Material = GetComponent<Renderer>().material;
        SpinSpeed = new Vector3(Random.Range(0.0f, 360f), Random.Range(0.0f, 360f), Random.Range(0.0f, 360f));
        ScaleSpeed = 20;
        transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        transform.eulerAngles += SpinSpeed * Time.deltaTime;
        m_Material.SetTextureOffset("_MainTex",new Vector2(1.42f, timer/2.5f));
        timer += 2 * Time.deltaTime;
        transform.localScale += Time.deltaTime * new Vector3(ScaleSpeed, ScaleSpeed, ScaleSpeed);
        ScaleSpeed = 20/(1+(timer * timer));
        if (timer > 2)
        {
            Destroy(this.gameObject); 
        }
    }
}
