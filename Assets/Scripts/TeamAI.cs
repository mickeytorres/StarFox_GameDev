using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAI : MonoBehaviour
{
    public Vector3 myDestination;
    float health = 10f;
    float hitChance;

    void Start() {
        hitChance = Random.Range(0f, 100f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(myDestination);
        transform.Translate(0, 0, 5f * Time.deltaTime); //swim 5 metres/second

        Debug.DrawLine(transform.position, myDestination, Color.blue);

        //pick a new destination once we reach the current destination 
        if (Vector3.Distance(transform.position, myDestination) < 5f) {
            PickNewDestination();
        }

        LoseHealth();
    }

    void LoseHealth() {
        if (hitChance >= 95) {
            health -= 0.1f;
        }
        Debug.Log("Team health " + health);

        hitChance = Random.Range(0f, 100f);

        if (health <= 0) {
            Debug.Log("Team member down");
            Destroy(gameObject);
        }
    }

    public void PickNewDestination () {
        myDestination = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f);
    }
}
