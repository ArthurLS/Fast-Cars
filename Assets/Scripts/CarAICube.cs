using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAICube : MonoBehaviour {

    public CarAI tesla;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.name + " is asking to switch target");
        if (other.CompareTag("Tesla"))
        {
            tesla.SwitchMarker(this.gameObject);
        }
    }
}
