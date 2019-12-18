using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetForward : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * 10 * Time.deltaTime;
    }
}
