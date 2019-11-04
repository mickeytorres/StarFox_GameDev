using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform mySphere;
    float verticalAngle = 0f;

    // Update is called once per frame
    void Update()
    {
        //step 1: declare a ray, plug mouse's screenspace pixel coordinate
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //step 2: declare mouse ray distance 
        float mouseRayDistance = 100f;

        //step2b: declare a blank raycast hit variable
        RaycastHit rayHit = new RaycastHit();

        //step 3: debug draw the raycast
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * mouseRayDistance, Color.blue);

        //step 4: shoot the raycast
        if (Physics.Raycast(mouseRay, out rayHit, mouseRayDistance)) {
            mySphere.position = rayHit.point;

            //want the raycast to ignore any blasts it hits
            if (rayHit.collider.gameObject.tag == "blast") {
                
            }
        }

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        
        //returns 0 if we aren't moving the mouse
        float mouseX = Input.GetAxis("Mouse X"); //horizontal mouse velocity
        float mouseY = Input.GetAxis("Mouse Y"); //vertical mouse velocity 

        transform.parent.Rotate(0f, mouseX * 5f, 0f);

        verticalAngle -= mouseY * 10f; 
        verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);

        // X = pitch, Y = yaw, Z = Roll 
        transform.localEulerAngles = new Vector3(verticalAngle, transform.localEulerAngles.y, 0f);
    }
}


