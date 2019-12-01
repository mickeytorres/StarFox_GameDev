using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float SecondsTillDestroy;
    public GameObject DestroyWhat;

    void Start()
    {
        StartCoroutine(Timer());
        if (DestroyWhat == null)
        {
            DestroyWhat = this.gameObject;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(SecondsTillDestroy);
        Destroy(DestroyWhat);
    }
}
