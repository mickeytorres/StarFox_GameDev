using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: 
//intent: 

public class ScreenShake : MonoBehaviour {
    
    public IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalCamPos = transform.localPosition;

        float timeElapsed = 0f;

        while (timeElapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalCamPos.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalCamPos;
    }

}
