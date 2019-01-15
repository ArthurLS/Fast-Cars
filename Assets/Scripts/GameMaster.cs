using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMaster : MonoBehaviour {

    private static GameMaster instance;

    GameObject nextCheckPoint;
    public List<GameObject> checkpoints;
    int nextCheck;
    int laps;
    int lapsToDo;
    public Text displayLaps;
    public Text displayCheck;

    void Start()
    {
        lapsToDo = 3;
        laps = 0;
        displayLaps.text = "Laps: " + laps + "/" + lapsToDo;
        nextCheck = 0;
        nextCheckPoint = checkpoints[0];
        displayCheck.text = "CheckPoints: " + nextCheck + "/" + checkpoints.Count;
    }

    void Update()
    {

    }

    public void  validCheckpoint (GameObject check)
    {
        if (nextCheckPoint.Equals(check)) { 
            nextCheck++;

            if(nextCheck < checkpoints.Count)
            {
                nextCheckPoint = checkpoints[nextCheck];
            }
            else
            {
                nextCheck = 0;
                nextCheckPoint = checkpoints[0];
                laps++;

            }

            displayLaps.text = "Laps: " + laps + "/" + lapsToDo;
            displayCheck.text = "CheckPoints: " + nextCheck + "/" + checkpoints.Count;
        }

        //Debug.Log("Next Checkpoint: " + checkpoints[nextCheck].name);
    }
}
