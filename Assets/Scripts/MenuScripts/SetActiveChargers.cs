using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveChargers : MonoBehaviour
{
    [SerializeField]
    private GameObject player3;

    [SerializeField]
    private GameObject player4;

    public void TwoPlayerGame()
    {
        player3.SetActive(false);
        player4.SetActive(false);
    }

    public void ThreePlayerGame()
    {
        player4.SetActive(false);
    }

    public void ResetPlayers()
    {
        player3.SetActive(true);
        player4.SetActive(true);
    }
}
