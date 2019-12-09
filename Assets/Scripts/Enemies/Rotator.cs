using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    private Rigidbody rb;
    public float rotationSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.angularVelocity = Random.insideUnitSphere * rotationSpeed;
    }
}
