using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCount : MonoBehaviour
{
    [SerializeField]
    private Text player1Text;

    [SerializeField]
    private Text player2Text;

    [SerializeField]
    private Text player3Text;

    [SerializeField]
    private Text player4Text;

	void FixedUpdate()
    {
        UpdateInfo();
	}

    private void UpdateInfo()
    {
        if (FloorColorChanges.player1Count != 0)
        {
            player1Text.text = "Player 1 Total: " +
                FloorColorChanges.player1Count.ToString();
        }
        else
            player1Text.text = string.Empty;

        if (FloorColorChanges.player2Count != 0)
        {
            player2Text.text = "Player 2 Total: " +
                FloorColorChanges.player2Count.ToString();
        }
        else
            player2Text.text = string.Empty;

        if (FloorColorChanges.player3Count != 0)
        {
            player3Text.text = "Player 3 Total: " +
                FloorColorChanges.player3Count.ToString();
        }
        else
            player3Text.text = string.Empty;

        if (FloorColorChanges.player4Count != 0)
        {
            player4Text.text = "Player 4 Total: " +
                FloorColorChanges.player4Count.ToString();
        }
        else
            player4Text.text = string.Empty;
    }
}
