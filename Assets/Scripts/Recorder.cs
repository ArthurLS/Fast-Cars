using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.Animations;

public class Recorder : MonoBehaviour
{
    public AnimationClip clip;
    public bool record = false;

    public GameObject recordedObject;

    private GameObjectRecorder recorder;

    void Start()
    {
        recorder = new GameObjectRecorder(recordedObject);
        recorder.BindComponentsOfType<Transform>(recordedObject, true);

    }

    void LateUpdate()
    {
        if (clip == null)
            return;

        if (record)
        {
            //Debug.Log("Take Snapshot");
            recorder.TakeSnapshot(Time.deltaTime);
        }
        else if (recorder.isRecording)
        {
            Debug.Log("Save Clip");
            recorder.SaveToClip(clip);
            recorder.ResetRecording();
        }
    }
}