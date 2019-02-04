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

    public Light hlRight;
    public Light hlLeft;

    public bool isOnLight = false;

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

    public void StopRecord()
    {
        isPlaying = false;
        vcr.Stop();
    }

    void Update()
    {
        if (isOnLight)
        {
            hlRight.intensity = 7;
            hlLeft.intensity = 7;
        }
        else
        {
            hlRight.intensity = 0;
            hlLeft.intensity = 0;
        }
    }

    public void setLightOn()
    {
        isOnLight = true;
    }
    public void setLightOff()
    {
        isOnLight = false;
    }
}
