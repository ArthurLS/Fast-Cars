using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyCamTarget : MonoBehaviour {

    public GameObject TheVehicule;
    private float VehiculeX;
    private float VehiculeY;
    private float VehiculeZ;

	// Update is called once per frame
	void Update () {
        VehiculeX = TheVehicule.transform.eulerAngles.x;
        VehiculeY = TheVehicule.transform.eulerAngles.y;
        VehiculeZ = TheVehicule.transform.eulerAngles.z;

        transform.eulerAngles = new Vector3(VehiculeX - VehiculeX, VehiculeY, VehiculeZ - VehiculeZ);
    }
}
