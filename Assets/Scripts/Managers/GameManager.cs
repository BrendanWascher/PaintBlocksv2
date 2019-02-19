using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //this class is based off of the Tank Tutorials "TankManager"
    //class in the tanks tutorial on the Unity Website
    // edits to this will be commented on 

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject confirmationPanel;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private Text winnerText;

    public int numberOfRoundsToWin = 3;
    public float startDelay = 3f;
    public float endDelay = 3f;

    public GameObject floor;
    public CameraController cameraController;
    public Text messageText;
    public GameObject camera;   // this is needed to pass to the text attached to player
    public GameObject playerPrefab;
    public PlayerManager[] players;

    // this is so all four instances of players are kept in the game manager so there 
    // can be up to four players without having to manually change it from the inspector
    // and can be instead changed from the main menu
    [HideInInspector] public static int numberOfPlayers = 2;

    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;

    // this is to locally keep track of the players score
    private int[] playersScore = new int[4];
    private bool isPaused = false;
    private bool pausePressed = false;
    private bool isRoundOver = false;

    void Start ()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        pausePanel.SetActive(false);
        endGamePanel.SetActive(false);

        SpawnAllPlayers();
        SetCameraTargets();
        SetScores();
        OverlapChecker.Setup();

        StartCoroutine(GameLoop());
	}

    private void Update()
    {
        UpdatePlayerLocations();
        CheckPause();
    }

    private void SpawnAllPlayers()
    {
        // this is changed so you only spawn in the number of players
        // actually playing instead of all instances of players
        for(int i = 0; i < numberOfPlayers; i++)
        {
            players[i].playerInstance =
                Instantiate(playerPrefab, players[i].playerSpawnPoint.position,
                players[i].playerSpawnPoint.rotation) as GameObject;
            players[i].playerNumber = i + 1;
            players[i].camera = camera;
            players[i].Setup();
        }
    }

    private void SetCameraTargets()
    {
        // changed to number of players instead of instances of players
        // so the camera doesn't try to make targets of null values
        Transform[] targets = new Transform[numberOfPlayers];

        for(int i = 0; i < targets.Length; i++)
        {
            targets[i] = players[i].playerInstance.transform;
        }

        cameraController.targets = targets;
    }

    // set local values of player score to better iterate through 
    // in get round winner
    private void SetScores()
    {
        playersScore[0] = FloorColorChanges.player1Count;
        playersScore[1] = FloorColorChanges.player2Count;
        playersScore[2] = FloorColorChanges.player3Count;
        playersScore[3] = FloorColorChanges.player4Count;
    }

    
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if(gameWinner != null)
        {
            //SceneManager.LoadScene(0);
            endGamePanel.SetActive(true);
            winnerText.text = EndGameText();
            GameTimer.isPaused = true;
            isRoundOver = true;
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }
    

    private IEnumerator RoundStarting()
    {
        ResetFloor();   //needed to reset map back to original values
        ResetAllPlayers();
        DisablePlayerControl();
        isRoundOver = true;

        cameraController.SetStartPositionAndSize();
        roundNumber++;
        messageText.text = "Round " + roundNumber;

        GameTimer.isPaused = false;
        GameTimer.startTimer = startDelay;
        yield return startWait;
    }

    private IEnumerator RoundPlaying()
    {
        EnablePlayerControl();
        isRoundOver = false;

        messageText.text = string.Empty;

        while(!TimeIsUp())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        DisablePlayerControl();
        isRoundOver = true;

        roundWinner = GetRoundWinner();

        if(roundWinner != null)
        {
            roundWinner.numberOfWins++;
        }

        gameWinner = GetGameWinner();

        string message = EndMessage();
        messageText.text = message;

        roundWinner = null;

        GameTimer.startTimer = endDelay;   //reset start timer
        GameTimer.isTimeUp = false; // reset so that time is not up
        yield return endWait;
    }

    private bool TimeIsUp()
    {
        return GameTimer.isTimeUp;  // check vlue of is time up from game timer
    }

    // this has been majorly changed from the tanks tutorial 
    // instead of checking if theres one tank left, it checks the scores
    // after time is up
    private PlayerManager GetRoundWinner()
    {
        bool isATie = false;    // initially say that there is not a tie
        int winnerNumber = 0;   // initially set player 1 to ther winning number
        SetScores();    // get the scores
        for (int i = 0; i < playersScore.Length; i++)   // go through all the scores
        {
            // there will always be a tie if the i != 0 is included
            if (playersScore[winnerNumber] == playersScore[i] && i != 0)
            {
                isATie = true;  // if there is a tie, set isATie to true
            }
            // check if the current playerscore is greater than the score of that of 
            //the current winner
            else if (playersScore[i]>playersScore[winnerNumber])
            {
                winnerNumber = i;   // set that player to the winning number
                isATie = false; // there is no longer a tie at the moment
            }
        }

        if (!isATie)    // if there isn't a tie
            return players[winnerNumber];   // return the player of the winning number
        else     // if there is a tie
            return null;    // return the playerManger value of null
    }

    private PlayerManager GetGameWinner()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].numberOfWins == numberOfRoundsToWin)
            {
                return players[i];
            }
        }

        return null;
    }

    private string EndMessage()
    {
        string message = "DRAW!";

        if(roundWinner != null)
        {
            message = roundWinner.coloredPlayerText + " wins the round!";
        }

        message += "\n\n";

        for(int i = 0; i < numberOfPlayers; i++)
        {
            message += players[i].coloredPlayerText + " has " +
                players[i].numberOfWins + " wins.\n";
        }

        if(gameWinner != null)
        {
            message = gameWinner.coloredPlayerText + " has won the game!";
        }

        return message;
    }

    private void ResetAllPlayers()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            players[i].Reset();
        }
    }

    // since the floor changes in this game as opposed to the tank tutorial
    // the floor must be reset each round
    private void ResetFloor()
    {
        // get all the instances of FloorColorChanges in the map
        FloorColorChanges[] map = floor.GetComponentsInChildren<FloorColorChanges>();
        for(int i = 0; i < map.Length; i++)
        {
            map[i].Reset(); // reset all those instances
        }

        // reset all the player counts
        FloorColorChanges.player1Count = 0;
        FloorColorChanges.player2Count = 0;
        FloorColorChanges.player3Count = 0;
        FloorColorChanges.player4Count = 0;
    }

    private void EnablePlayerControl()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            players[i].EnableControl();
        }
    }

    private void DisablePlayerControl()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            players[i].DisableControl();
        }
    }

    private void UpdatePlayerLocations()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            float X = players[i].GetXCoord();
            float Z = players[i].GetZCoord();
            OverlapChecker.playerLocations[i, 0] = X;
            OverlapChecker.playerLocations[i, 1] = Z;
        }
    }

    private void CheckPause()
    {
        if (!pausePressed && !isRoundOver)
        {
            if (Input.GetAxis("Pause") > 0)
            {
                // this is so it doesn't think pause is being
                // pressed multiple times in a row
                pausePressed = true;

                if (!isPaused)
                    PauseGame();
                else if (isPaused)
                    UnPauseGame();
            }
        }
        else if(Input.GetAxis("Pause") == 0)
        {
            pausePressed = false;
        }
    }

    private void PauseGame()
    {
        DisablePlayerControl();
        isPaused = true;
        GameTimer.isPaused = true;
        PlayerSpray.isPaused = true;
        pausePanel.SetActive(true);
    }

    private void UnPauseGame()
    {
        EnablePlayerControl();
        isPaused = false;
        GameTimer.isPaused = false;
        PlayerSpray.isPaused = false;
        confirmationPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private string EndGameText()
    {
        string message = "Player "+gameWinner.playerNumber + " wins!";
        return message;
    }

    public void Replay()
    {
        //endGamePanel.SetActive(false);
        //reload this scene
        SceneManager.LoadScene(1);
    }
}
