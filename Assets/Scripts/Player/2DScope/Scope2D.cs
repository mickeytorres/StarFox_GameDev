using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope2D : MonoBehaviour
{
    public Transform target;
    public Camera cam;
    public RectTransform MyTransform;

    void Start()
    {
    }

    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        MyTransform.position = screenPos;
    }
}
