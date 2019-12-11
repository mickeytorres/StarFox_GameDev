using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on an empty gameObject
//intent: spawn a bunch of small spheres in the back to look like stars

public class BackParallax : MonoBehaviour {
    public GameObject starPrefab;
    public GameObject backStarsHolder;

    // Start is called before the first frame update
    void Start() {
        
        for (int i = 0; i < 150; i++) {
            //give each star a random position on screen
            Vector3 randomPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(-1, Screen.width + 1), Random.Range(0, Screen.height), Camera.main.farClipPlane/8));

            GameObject thisStar = Instantiate(starPrefab, randomPos, Quaternion.identity);

            //give each star a random scale within a specific range
            float randomScale = Random.Range(0.4f, 0.5f);
            thisStar.transform.localScale += new Vector3(randomScale, randomScale, randomScale);
            thisStar.transform.parent = backStarsHolder.transform;
        }
    }
}
