using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapChecker : MonoBehaviour
{
    [HideInInspector] public static float[,] playerLocations = new float[4,2];

    public static void Setup()
    {
        playerLocations = new float[GameManager.numberOfPlayers, 2];

        for(int i = 0; i< GameManager.numberOfPlayers; i++)
        {
            playerLocations[i, 0] = 0;
            playerLocations[i, 1] = 0;

            Debug.Log("New Player Added");
        }
    }

    public static bool CheckOverlap(int playerNumber, string direction, float newCoord, float curCoord)
    {
        for(int i = 0; i < GameManager.numberOfPlayers; i++)
        {
            if(direction == "X")
            {
            if (newCoord == playerLocations[i, 0] && 
                curCoord == playerLocations[i,1])
                   return false;
            }
            else if(direction == "Z")
            {
                if (newCoord == playerLocations[i, 1] &&
                    curCoord == playerLocations[i, 0])
                        return false;
            }
        }
        return true;
    }
}
