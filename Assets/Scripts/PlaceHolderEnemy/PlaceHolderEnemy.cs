using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderEnemy : MonoBehaviour
{
    public float Z;
    public float X;
    public float Speed;
    public Transform Player;
    public int Mode;
    public float timer = 10;
    public GameObject MyParent;

    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        Z = transform.localPosition.z;
        Speed = 0.5f + Mathf.Abs(Z) * 0.1f;
    }

    void Update()
    {
        Z -= Speed * Time.deltaTime;
        X = Z * Z;
        transform.localPosition = new Vector3(X, transform.localPosition.y - 5 * Time.deltaTime, Z + 20);
        transform.LookAt(Player);
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(MyParent);
        }
    }
}
