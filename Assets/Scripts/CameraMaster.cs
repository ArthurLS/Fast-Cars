using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaster : MonoBehaviour {

    private static CameraMaster instance;

    public Camera carCam;
    public string tagToFollow;

    List<Camera> cameras;
    public Camera currentCamera; 

    // Use this for initialization
    void Start () {
        carCam.enabled = true;
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Camera>().enabled = false;
        }

    }

    public void switchCamera(Camera cam, string tag)
    {
        //Debug.Log("We are switching the camera to: "+cam.name);
        if(tag == tagToFollow)
        {
            currentCamera.enabled = false;
            currentCamera = cam;
            if (!carCam.enabled)
            {
                currentCamera.enabled = true;
            }
        }

    }

    // Update is called once per frame
    void Update ()
    {
        // Enables Third person camera
        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("Third Person Camera Activated");
            carCam.enabled = true;
            currentCamera.enabled = false;

        }
        // Enables Omniscient person camera
        else if (Input.GetKey(KeyCode.O))
        {
            Debug.Log("Omniscient Camera Activated");
            currentCamera.enabled = true;
            carCam.enabled = false;
        }
    }

}
