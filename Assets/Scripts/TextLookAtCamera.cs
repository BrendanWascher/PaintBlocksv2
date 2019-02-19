using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLookAtCamera : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public PlayerController playerController;

    private void Start()
    {
        playerController.GetComponents<PlayerController>();
        text.color = playerController.playerMaterial.color;
        text.text = "Player " + playerController.playerNumber;
    }

    private void FixedUpdate()
    {
        LookAtCamera();
	}

    // seperate function in case other code needs to be added so not to
    // clog up fixedupdate
    private void LookAtCamera()
    {
        transform.LookAt(playerController.camera.transform);
        transform.Rotate(0, 180f, 0);
    }

}
