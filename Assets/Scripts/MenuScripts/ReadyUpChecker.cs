using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUpChecker : MonoBehaviour
{
    [SerializeField]
    private PlayerReadyUp player1;

    [SerializeField]
    private PlayerReadyUp player2;

    [SerializeField]
    private PlayerReadyUp player3;

    [SerializeField]
    private PlayerReadyUp player4;

    [SerializeField]
    private Button playButton;

    private int numberOfPlayers;

    private void Start()
    {
        playButton.GetComponent<Button>();
    }

    void FixedUpdate ()
    {
        CheckPlaying();
        CheckReadyUp();
	}

    void CheckReadyUp()
    {
        switch (numberOfPlayers)
        {
            case 2:
                if (player1.isReady && player2.isReady)
                {
                    playButton.gameObject.SetActive(true);
                }
                break;

            case 3:
                if (player1.isReady && player2.isReady 
                    && player3.isReady)
                {
                    playButton.gameObject.SetActive(true);
                }
                break;

            case 4:
                if (player1.isReady && player2.isReady
                    && player3.isReady && player4.isReady)
                {
                    playButton.gameObject.SetActive(true);
                }
                break;

            default:
                break;
        }
    }

    void CheckPlaying()
    {
        numberOfPlayers = GameManager.numberOfPlayers;
    }
}
