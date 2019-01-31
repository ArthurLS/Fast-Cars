using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scoreboard : MonoBehaviour {
   
    private static Scoreboard instance;

    public Text bestTimeDisplayed;
    List<float> scoreboard; 

	// Use this for initialization
	void Start () {
        this.scoreboard = new List<float>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void addToBoard(float newtime)
    {
        Debug.Log("New Time");
        scoreboard.Add(newtime);
        scoreboard.Sort();
        bestTimeDisplayed.text = "Best: " + scoreboard[0] + "s";
    }
}
