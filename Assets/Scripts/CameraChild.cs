using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChild : MonoBehaviour {

    public CameraMaster camMaster;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Camera Triggered");
        if (other.CompareTag("Player"))
        {
            camMaster.switchCamera(this.gameObject.GetComponent<Camera>());
        }
    }

}
