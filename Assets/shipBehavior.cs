using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipBehavior : MonoBehaviour {

    Rigidbody rigidbody = null;
    Vector3 euler_y;
    Vector3 euler_y2;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();

        //Set the axis the Rigidbody rotates in (100 in the y axis)
        euler_y = new Vector3(0, 0, 156);
        euler_y2 = new Vector3(0, 0, -156);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        { 
            rigidbody.AddForce(transform.up * 10, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddForce(transform.up * -10, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Quaternion deltaRotation = Quaternion.Euler(euler_y2 * Time.deltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Quaternion deltaRotation = Quaternion.Euler(euler_y * Time.deltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
    }
}
