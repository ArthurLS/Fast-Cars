using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scoreboard : MonoBehaviour {
   
    private static Scoreboard instance;

    List<float> times;

	// Use this for initialization
	void Start () {
        this.times = new List<float>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void addToBoard(float newtime)
    {
        Debug.Log("New Time" + newtime);
        times.Add(newtime);
        times.Sort();
        Debug.Log("AddToBoard BestTime" +times[0]);
    }

    public float getBestTime()
    {
        if (times.Count > 0)
        {
            return times[0];
        }
        return -1f;
    }
}
