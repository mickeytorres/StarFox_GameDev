using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackStar : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.right * Time.deltaTime * 2);

        CheckPosition();
    }

    void CheckPosition() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

        Vector3 currPos = transform.position;

		if (screenPos.x > Screen.width) {
            transform.position = new Vector3(-currPos.x, currPos.y, currPos.z);
		}
    }
}
