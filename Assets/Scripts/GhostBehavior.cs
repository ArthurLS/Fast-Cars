using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour {

    private static GhostBehavior instance;

    InputVCR vcr;
    Vector3 lastPos;
    Quaternion lastRot;

    Vector3 targPos;
    Quaternion targRot; 
    public float damping = 10f;
    private bool isPlaying;


    void Awake()
    {
        vcr = GetComponent<InputVCR>();

        targPos = transform.position;
        targRot = transform.rotation;
        lastPos = transform.position;
        lastRot = transform.rotation;
        isPlaying = false;
    }

    public void PlayRecord(Recording recording)
    {
        if (recording != null)
        {
            isPlaying = true;
            vcr.Play(recording, 0f);
        }
    }
}
