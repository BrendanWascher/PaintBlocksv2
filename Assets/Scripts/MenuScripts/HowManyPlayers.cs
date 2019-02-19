using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowManyPlayers: MonoBehaviour
{
    public void NumberOfPlayers(int numberOfPlayers)
    {
        GameManager.numberOfPlayers = numberOfPlayers;
    }
}
