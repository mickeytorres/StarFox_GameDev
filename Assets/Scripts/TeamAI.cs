using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAI : MonoBehaviour
{
    public Vector3 myDestination;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(myDestination);
        transform.Translate(0, 0, 2f * Time.deltaTime); 

        Debug.DrawLine(transform.position, myDestination, Color.blue);

        //pick a new destination once we reach the current destination 
        if (Vector3.Distance(transform.position, myDestination) < 5f) {
            PickNewDestination();
        }
    }

    public void PickNewDestination () {
        myDestination = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f);
    }
}
