using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on any object that is part of the Main Menu parallax effect
//intent: parallax effect on the main menu with three different layers

public class BackStar : MonoBehaviour {

    float asteroidFrontSpeed = 4.25f;
    float asteroidMiddleSpeed = 2.75f;

    // Update is called once per frame
    void Update() {

        //the small spheres in the very back. Move the slowest. 
        if (gameObject.tag == "StarBack") {
            MoveStar();
        }

        //medium-sized asteroids in the mid-ground of the background. Move the second slowest.
        if (gameObject.tag == "AsteroidMiddle") {
            MoveAsteroidMiddle();
        }

        //largest asteroids in the very front of the background. Move the fastest.
        if (gameObject.tag == "AsteroidFront") {
            MoveAsteroidFront();
        }

        CheckPosition();
    }

    //three functions to handle moving different objects in the background at different speeds
    void MoveStar() {
        transform.Translate(Vector3.right * Time.deltaTime * 2f, Space.World);
    }

    void MoveAsteroidMiddle() {
        transform.Translate(Vector3.right * Time.deltaTime * asteroidMiddleSpeed, Space.World);
    }

    void MoveAsteroidFront() {
        transform.Translate(Vector3.right * Time.deltaTime * asteroidFrontSpeed, Space.World);
    }

    //check positions of objects to make them wrap around
    void CheckPosition() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 currPos = transform.position;

		if (screenPos.x > (Screen.width + 1)) {
            transform.position = WrapAroundPos(currPos);
            transform.rotation = Random.rotation;

            if (gameObject.tag == "AsteroidMiddle") {
                asteroidMiddleSpeed = Random.Range(2.25f, 2.75f);
            }
             
            if (gameObject.tag == "AsteroidFront") {
                asteroidFrontSpeed = Random.Range(3f, 4.5f);
            }
		}
    }

    //the furthest stars in the back will wrap around at the same y positions.
    //assign new y positions to the two types of asteroids
    Vector3 WrapAroundPos(Vector3 currPos) {
        Vector3 newPos = new Vector3(-currPos.x, currPos.y, currPos.z);
        
        if (gameObject.tag == "AsteroidMiddle" || gameObject.tag == "AsteroidFront") {
            newPos = new Vector3(-currPos.x, Random.Range(-4, 6), currPos.z);    
        }

        return newPos;
    }
}
