using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMaster : MonoBehaviour {

    private static GameMaster instance;

    public Scoreboard board;
    public CameraMaster cameraMaster;

    GameObject nextCheckPoint;
    public List<GameObject> checkpoints;
    int nextCheck;
    public Text displayCheck;
    public Text displayTime;
    public Text displayBestTime;

    // Fields use for countdown
    public GameObject displayCountDown;
    public int startCountdown;

    public Button buttonReplay;
    public Button buttonGhost;
    public Button buttonVersus;
    public Button buttonTime;
    public Button buttonBack;
    public Button buttonExit;

    GameMode mode;
    Scoreboard scoreboard;
    float startTime;

    public ShipBehavior ship;
    public GhostBehavior ghost;
    public CarAI tesla;

    public GameObject teslaGO;
    public GameObject shipGO;
    public GameObject ghostGO;
    public GameObject menuGO;
    public GameObject uiTextGO;
    public GameObject endScreenGO;

    public GameObject light1GO;
    public GameObject light2GO;

    Quaternion light1StartingRot;
    Quaternion light2StartingRot;

    Recording bestRecord;

    Vector3 shipStartingPos;
    Vector3 teslaStartingPos;
    Vector3 ghostStartingPos;

    Quaternion shipStartingRot;
    Quaternion teslaStartingRot;
    Quaternion ghostStartingRot;

    Material ghotStartingMat;
    Material shipStartingMat;

    // Time of the palyer when the race is playing
    float playerLapTime;

    void Start()
    {
        shipStartingMat= shipGO.GetComponentInChildren<Renderer>().material;
        ghotStartingMat = ghostGO.GetComponentInChildren<Renderer>().material;

        shipStartingPos = shipGO.transform.position;
        teslaStartingPos = teslaGO.transform.position;
        ghostStartingPos = ghostGO.transform.position;

        shipStartingRot = shipGO.transform.rotation;
        teslaStartingRot = teslaGO.transform.rotation;
        ghostStartingRot = ghostGO.transform.rotation;

        light1StartingRot =light1GO.transform.rotation;
        light2StartingRot = light2GO.transform.rotation;

        scoreboard = this.GetComponent<Scoreboard>();

        mode = GameMode.Menu;

        uiTextGO.SetActive(false);
        menuGO.SetActive(true);
        endScreenGO.SetActive(false);

        buttonReplay.onClick.AddListener(SetReplayMode);
        buttonGhost.onClick.AddListener(SetGhostMode);
        buttonVersus.onClick.AddListener(SetVersusMode);
        buttonTime.onClick.AddListener(SetTimeMode);
        buttonBack.onClick.AddListener(SetMenuMode);
        buttonExit.onClick.AddListener(ExitGame);

    }

    void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }

    void SetGhostMode()
    {
        Debug.Log("Vs Ghost Mode Activated");
        InitScene();

        teslaGO.SetActive(false);
        shipGO.SetActive(true);
        ghostGO.SetActive(true);
        menuGO.SetActive(false);
        uiTextGO.SetActive(true);
        endScreenGO.SetActive(false);
        StartCoroutine(PlayCountdown(()=>
        {
            mode = GameMode.Ghost;
            startTime = Time.time;
            ship.StartPlaying();
            if (bestRecord != null)
            {
                ghost.PlayRecord(bestRecord);
            }
        }));
    }
   
   void SetTimeMode()
    {
        InitScene();
        Debug.Log("Vs Time Mode Activated");
        teslaGO.SetActive(false);
        shipGO.SetActive(true);
        ghostGO.SetActive(false);
        menuGO.SetActive(false);
        uiTextGO.SetActive(true);
        endScreenGO.SetActive(false);
        StartCoroutine(PlayCountdown(()=>
        {
            mode = GameMode.Time;
            startTime = Time.time;
            ship.StartPlaying();
        }));
    }
   
   void SetVersusMode()
    {
        Debug.Log("Vs AI Mode Activated");
        InitScene();

        teslaGO.SetActive(true);
        teslaGO.GetComponent<CarAI>().enabled = true;

        shipGO.SetActive(true);
        ghostGO.SetActive(false);
        menuGO.SetActive(false);
        uiTextGO.SetActive(true);
        endScreenGO.SetActive(false);
        StartCoroutine(PlayCountdown(() =>
        {
            mode = GameMode.Versus;
            startTime = Time.time;
            ship.StartPlaying();
            tesla.Play();
        }));
    }

    void SetReplayMode()
    {
        Debug.Log("Replay Mode Activated");
        mode = GameMode.Replay;
        ship.StopPlaying();
        InitScene();

        ghost.setLightOn();

        teslaGO.SetActive(false);
        shipGO.SetActive(false);
        ghostGO.SetActive(true);
        menuGO.SetActive(false);
        uiTextGO.SetActive(false);
        endScreenGO.SetActive(false);

        // change lights orientation
        float l1EulerY = light1GO.transform.localEulerAngles.y;
        float l2EulerY = light2GO.transform.localEulerAngles.y;
        float l1EulerZ = light1GO.transform.localEulerAngles.z;
        float l2EulerZ = light2GO.transform.localEulerAngles.z;
        light1GO.transform.rotation = Quaternion.Euler(20, l1EulerY, l1EulerZ);
        light2GO.transform.rotation = Quaternion.Euler(20, l2EulerY, l2EulerZ);

        // change camera
        cameraMaster.SetOmniscient();


        if(bestRecord != null)
        {

            ghostGO.GetComponentInChildren<Renderer>().material = shipStartingMat;
            ghost.PlayRecord(bestRecord);
            StartCoroutine(WaitForReplay(() =>
            {
                if(mode == GameMode.Replay)
                {
                    endScreenGO.SetActive(true);
                    Debug.Log("End of the Replay!");
                }
            }));
        }

    }

    void SetMenuMode()
    {
        ghost.setLightOff();

        Debug.Log("Menu Mode Activated");
        mode = GameMode.Menu;
        ship.StopPlaying();
        ghost.StopRecord();
        InitScene();

        teslaGO.SetActive(true);
        shipGO.SetActive(true);
        ghostGO.SetActive(true);
        menuGO.SetActive(true);
        uiTextGO.SetActive(false);
        endScreenGO.SetActive(false);

    }

    void InitScene()
    {
        nextCheck = 0;
        nextCheckPoint = checkpoints[0];
        displayCheck.text = "CheckPoints: " + nextCheck + "/" + checkpoints.Count;
        displayTime.text = "Time: 00:00:00";

        shipGO.transform.position = shipStartingPos;
        teslaGO.transform.position = teslaStartingPos;
        ghostGO.transform.position = ghostStartingPos;

        shipGO.transform.rotation = shipStartingRot;
        teslaGO.transform.rotation = teslaStartingRot;
        ghostGO.transform.rotation = ghostStartingRot;

        // reset light orientation
        light1GO.transform.rotation = light1StartingRot;
        light2GO.transform.rotation = light2StartingRot;

        // reset Replay
        ghost.StopRecord();
        ghostGO.GetComponentInChildren<Renderer>().material = ghotStartingMat;
        cameraMaster.SetThirdPerson();

        //reset IA
        tesla.Stop();
    }

    void Update()
    {
        switch (mode)
        {
            case GameMode.Time:
                UpdateTime();
                break;
            case GameMode.Ghost:
                UpdateTime();
                break;
            case GameMode.Menu:
                break;
            case GameMode.Versus:
                UpdateTime();
                break;
            case GameMode.Replay:
                break;
        }
    }

    void UpdateTime()
    {
        playerLapTime = Time.time - startTime;

        displayTime.text = "Time: " + ParseTimeToString(playerLapTime);
    }

    public void validCheckpoint (GameObject check)
    {
        if (nextCheckPoint.Equals(check)) {
            nextCheck++;

            if (nextCheck < checkpoints.Count)
            {
                if(nextCheck == 2)
                {
                    //FinishRace();
                }

                // Move this code where to the end line
                nextCheckPoint = checkpoints[nextCheck];
            }
            else
            {
                FinishRace();

                nextCheck = 0;
                nextCheckPoint = checkpoints[0];
            }

            if (check.name.Equals("Start Line"))
            {
                UpdateTime();
                ship.StartRecording();
            }

            displayCheck.text = "CheckPoints: " + nextCheck + "/" + checkpoints.Count;
        }

        //Debug.Log("Next Checkpoint: " + checkpoints[nextCheck].name);
    }


    void FinishRace()
    {
        playerLapTime = Time.time - startTime;
        board.addToBoard(playerLapTime);

        // check if the playerLapTime is the best time 
        if (playerLapTime <= scoreboard.getBestTime() || scoreboard.getBestTime().Equals(-1))
        {
            Debug.Log("Best Record");
            // update displayBest with new best time and save the record
            displayBestTime.text = "Best: " + ParseTimeToString(scoreboard.getBestTime());
            displayTime.text = "Time: " + ParseTimeToString(scoreboard.getBestTime());
            bestRecord = ship.GetRecording();
            Debug.Log(bestRecord.ToString());
        }

        endScreenGO.SetActive(true);
        mode = GameMode.Menu;

    }

    IEnumerator WaitForReplay(System.Action callback)
    {
        yield return new WaitForSeconds(scoreboard.getBestTime());
        if (callback != null)
        {
            callback();
        }
    }

    IEnumerator PlayCountdown(System.Action callback)
    {

        yield return new WaitForSeconds(0.5f);
        Text countDownText = displayCountDown.GetComponent<Text>();
        for (int i = startCountdown; i > 0; i--)
        {
            displayCountDown.SetActive(true);
            countDownText.text = i + "";
            yield return new WaitForSeconds(1);
            displayCountDown.SetActive(false);

        }
        displayCountDown.SetActive(true);
        countDownText.text = "GO";
        yield return new WaitForSeconds(1);
        displayCountDown.SetActive(false);

        if (callback != null)
        {
            callback();
        }
    }

    enum GameMode
    {
        Ghost,
        Replay,
        Versus,
        Time, 
        Menu
    }

    static string ParseTimeToString(float time)
    {
        string min, sec, ms;

        if ((int)(time / 60) < 10) min = "0" + (int)(time / 60) + ":";
        else min = (int)(time/ 60) + ":";

        if ((int)(time % 60) < 10) sec = "0" + (int)(time % 60) + ":";
        else sec = (int)(time % 60) + ":";

        if(((int) (time / 60) >= 1) && ((int)(time % 60) >= 40))
            ms = (time).ToString("00.00").Substring(4);
        else 
            ms = (time).ToString("00.00").Substring(3);

        return min + sec + ms;

    }
}
