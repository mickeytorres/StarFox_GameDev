using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackParallax : MonoBehaviour {
    public GameObject starPrefab;
    private GameObject[] starPrefabsArray = new GameObject[75];

    // Start is called before the first frame update
    void Start() {
        //spawn all on z position 12.1
        //give each star a random scale between 0.4 and 0.5
        
        //give each star a random position on screen
        for (int i = 0; i < 75; i++) {
            Vector3 randomPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane/8));

            GameObject thisStar = Instantiate(starPrefab, randomPos, Quaternion.identity);

            float randomScale = Random.Range(0.4f, 0.5f);

            thisStar.transform.localScale += new Vector3(randomScale, randomScale, randomScale);

            starPrefabsArray[i] = thisStar;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
