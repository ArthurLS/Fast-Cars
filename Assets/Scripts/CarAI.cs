using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour {

    private static CarAI instance;

    public GameMaster gm;

    public List<GameObject> markers;
    int nextMaker;
    public int lapsToDo;

    public float speedForward;
    float stepForward;
    public float speedRotation;
    bool isPlaying;
   
    public void Play()
    {
        nextMaker = 0;
        isPlaying = true;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (isPlaying)
        {
            stepForward = speedForward * Time.deltaTime;
            Transform target = markers[nextMaker].transform;
            transform.position = Vector3.MoveTowards(transform.position, target.position, stepForward);

            // Rotation
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            rotation *= Quaternion.Euler(0, 90, 0); // this adds a 90 degrees Y rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speedRotation);
        }
    }


    public void SwitchMarker(GameObject newMarker)
    {

        if (markers[nextMaker].Equals(newMarker))
        {
            nextMaker++;
            if ((nextMaker == markers.Count) && (lapsToDo != 0))
            {
                Debug.Log("LAP nextMarker: " + nextMaker + " count: " + markers.Count);
                nextMaker = 0;
                lapsToDo--;
            }
        }
    }

    public void Stop()
    {
        isPlaying = false;
        nextMaker = 0;
        lapsToDo = 1;
        stepForward = 0;
    }
}
