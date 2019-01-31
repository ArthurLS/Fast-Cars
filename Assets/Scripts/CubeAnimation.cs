using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimation : MonoBehaviour {

    public void attack()
    {
        GetComponent<Animation>().Play();
    }
}
