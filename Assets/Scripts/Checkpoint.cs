using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public GameMaster gm;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGGERED");
        if (other.CompareTag("Player"))
        {
            gm.validCheckpoint(this.gameObject);
        }
    }
}
