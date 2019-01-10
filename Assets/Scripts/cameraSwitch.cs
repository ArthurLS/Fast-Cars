using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour {
    public Camera carCam;

    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;

    // Use this for initialization
    void Start () {
        Debug.Log("We're here");
        carCam.enabled = true;
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false; 
        cam4.enabled = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Enables Third person camera
        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("P");
            carCam.enabled = true;
            cam1.enabled = false;
            cam2.enabled = false;
            cam3.enabled = false;
            cam4.enabled = false;
        }
        // Enable Omniscient Camera
        if (Input.GetKey(KeyCode.O))
        {
            Debug.Log("O");
            carCam.enabled = false;
        }

        if (!carCam.enabled)
        {
            //Debug.Log("Choosing which monitor");
            if (transform.position.x < 4 && transform.position.z > 2 && !cam2.enabled)
            {
                cam1.enabled = false;
                cam2.enabled = true;
                cam3.enabled = false;
                cam4.enabled = false;
                Debug.Log("Switch to 2");
            }
            else if (transform.position.x > 4 && transform.position.x < 12.3 && transform.position.z > 11.2 && !cam3.enabled)
            {
                cam1.enabled = false;
                cam2.enabled = false;
                cam3.enabled = true;
                cam4.enabled = false;
                Debug.Log("Switch to 3");
            }
            else if (transform.position.x > 12.3 && transform.position.z < 11.2 && transform.position.z > 3.5 && !cam4.enabled)
            {
                cam1.enabled = false;
                cam2.enabled = false;
                cam3.enabled = false;
                cam4.enabled = true;
                Debug.Log("Switch to 4");
            }
            else if (transform.position.x > 4 && transform.position.x < 12.3 && transform.position.z > 3 && transform.position.z < 8 && !cam1.enabled)
            {
                cam1.enabled = true;
                cam2.enabled = false;
                cam3.enabled = false;
                cam4.enabled = false;
                Debug.Log("Switch to 1");
            }
            Debug.Log("x: " + transform.position.x + " z: " + transform.position.z);
        }

        // x < 4 && z > 3
        // x > 4 && z > 10
        // x < 12 && z < 4


    }
}
