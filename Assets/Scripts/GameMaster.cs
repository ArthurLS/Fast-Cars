using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMaster : MonoBehaviour {

    private static GameMaster instance;


    public Scoreboard board;

    GameObject nextCheckPoint;
    public List<GameObject> checkpoints;
    int nextCheck;
    int laps;
    public int lapsToDo;
    public Text displayLaps;
    public Text displayCheck;
    public Text displayTime;

    public Button buttonReplay;
    public Button buttonGhost;
    public Button buttonVersus;
    public Button buttonTime;
    public Button buttonBack;

    GameMode mode;

    float playerTime;
    float lapTime;

    // fields use for the recording system
    public shipBehavior ship;
    public GhostBehavior ghost;

    public GameObject teslaGO;
    public GameObject shipGO;
    public GameObject ghostGO;
    public GameObject menuGO;
    public GameObject uiTextGO;

    Recording bestRecord;

    Vector3 shipStartingPos;
    Vector3 teslaStartingPos;
    Vector3 ghostStartingPos;

    Quaternion shipStartingRot;
    Quaternion teslaStartingRot;
    Quaternion ghostStartingRot;

    void Start()
    {
        shipStartingPos = shipGO.transform.position;
        teslaStartingPos = teslaGO.transform.position;
        ghostStartingPos = ghostGO.transform.position;

        shipStartingRot = shipGO.transform.rotation;
        teslaStartingRot = teslaGO.transform.rotation;
        ghostStartingRot = ghostGO.transform.rotation;

        mode = GameMode.Menu;

        uiTextGO.SetActive(false);
        menuGO.SetActive(true);

        buttonReplay.onClick.AddListener(SetReplayMode);
        buttonGhost.onClick.AddListener(SetGhostMode);
        buttonVersus.onClick.AddListener(SetVersusMode);
        buttonTime.onClick.AddListener(SetTimeMode);
        buttonBack.onClick.AddListener(SetMenuMode);

    }

    void SetGhostMode()
    {
        Debug.Log("Vs Ghost Mode Activated");
        ship.StartPlaying();
        mode = GameMode.Ghost;
        InitScene();

        teslaGO.SetActive(false);
        shipGO.SetActive(true);
        ghostGO.SetActive(true);
        menuGO.SetActive(false);
        uiTextGO.SetActive(true);

    }
    void SetVersusMode()
    {
        Debug.Log("Vs AI Mode Activated");
        mode = GameMode.Versus;
        ship.StartPlaying();
        InitScene();

        teslaGO.SetActive(true);
        teslaGO.GetComponent<CarAI>().enabled = true;

        shipGO.SetActive(true);
        ghostGO.SetActive(false);
        menuGO.SetActive(false);
        uiTextGO.SetActive(true);

    }
    void SetTimeMode()
    {
        Debug.Log("Vs Time Mode Activated");
        ship.StartPlaying();
        mode = GameMode.Time;
        InitScene();

        teslaGO.SetActive(false);
        shipGO.SetActive(true);
        ghostGO.SetActive(false);
        menuGO.SetActive(false);
        uiTextGO.SetActive(true);

    }
    void SetReplayMode()
    {
        Debug.Log("Replay Mode Activated");
        mode = GameMode.Replay;
        ship.StopPlaying();
        InitScene();

        teslaGO.SetActive(false);
        shipGO.SetActive(true);
        ghostGO.SetActive(false);
        menuGO.SetActive(false);
        uiTextGO.SetActive(false);
    }

    void SetMenuMode()
    {
        Debug.Log("Menu Mode Activated");
        mode = GameMode.Menu;
        ship.StopPlaying();
        InitScene();


        teslaGO.SetActive(true);
        shipGO.SetActive(true);
        ghostGO.SetActive(true);
        menuGO.SetActive(true);
        uiTextGO.SetActive(false);

    }

    void InitScene()
    {
        playerTime = Time.time;
        laps = 0;
        displayLaps.text = "Laps: " + laps + "/" + lapsToDo;
        nextCheck = 0;
        nextCheckPoint = checkpoints[0];
        displayCheck.text = "CheckPoints: " + nextCheck + "/" + checkpoints.Count;

        shipGO.transform.position = shipStartingPos;
        teslaGO.transform.position = teslaStartingPos;
        ghostGO.transform.position = ghostStartingPos;

        shipGO.transform.rotation = shipStartingRot;
        teslaGO.transform.rotation = teslaStartingRot;
        ghostGO.transform.rotation = ghostStartingRot;

    }


    void Update()
    {
        switch (mode)
        {
            case GameMode.Time:

                break;
            case GameMode.Ghost:

                break;
            case GameMode.Menu:

                break;
            case GameMode.Versus:

                break;
            case GameMode.Replay:

                break;
        }

        string minutes;
        string seconds;
        string ms;

        if ((int)((Time.time - playerTime) / 60) < 10) minutes = "0" + (int)((Time.time - playerTime) / 60) + ":";
        else minutes = (int)(Time.time - playerTime / 60) + ":";

        if ((int)((Time.time - playerTime) % 60) < 10) seconds = "0" + (int)((Time.time - playerTime) % 60) + ":";
        else seconds = (int)((Time.time - playerTime) % 60) + ":";

        ms = (Time.time - playerTime).ToString("00.00").Substring(3);

        displayTime.text = "Time: " + minutes + seconds + ms;

    }

    public void validCheckpoint (GameObject check)
    {
        float saveLap = 0.0f;
        if (nextCheckPoint.Equals(check)) {
            nextCheck++;

            if(check.name.Equals("Start Line"))
            {
                ship.StartRecording();
                saveLap = lapTime;
                lapTime = Time.time;
            }

            if (nextCheck < checkpoints.Count)
            {
                float timeDiff = Time.time - lapTime;
                Debug.Log("Checkpoint time: " + timeDiff.ToString("0.00") + " Sec");
                //board.addToBoard(timeDiff);
                if(nextCheck == 1)
                {
                    Debug.Log("Save record");
                    SaveRecord();
                    Debug.Log("run ghost");
                    RunGhost();
                }


                nextCheckPoint = checkpoints[nextCheck];
            }
            else
            {
                nextCheck = 0;
                nextCheckPoint = checkpoints[0];
                laps++;

                float timeDiff = Time.time - saveLap;

                Debug.Log("Lap Time " + timeDiff);

                board.addToBoard(timeDiff);
            }

            displayLaps.text = "Laps: " + laps + "/" + lapsToDo;
            displayCheck.text = "CheckPoints: " + nextCheck + "/" + checkpoints.Count;
        }

        //Debug.Log("Next Checkpoint: " + checkpoints[nextCheck].name);
    }

    public void SaveRecord()
    {
        bestRecord = ship.GetRecording();
        Debug.Log("Best Record");
        Debug.Log(bestRecord.ToString());
    }

    public void RunGhost()
    {
        ghost.PlayRecord(bestRecord);
    }

    private enum GameMode
    {
        Ghost,
        Replay,
        Versus,
        Time, 
        Menu
    }
}
